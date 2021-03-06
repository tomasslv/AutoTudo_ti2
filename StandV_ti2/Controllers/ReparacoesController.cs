using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandV_ti2.Data;
using StandV_ti2.Models;

namespace StandV_ti2.Controllers
{
    public class ReparacoesController : Controller
    {
        /// <summary>
        /// este atributo representa a base de dados do projeto
        /// </summary>
        private readonly ReparacaoDB _context;

        /// <summary>
        /// este atributo representa a base de dados do projeto
        /// </summary>
        public ReparacoesController(ReparacaoDB context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// esta variável recolhe os dados da pessoa q se autenticou
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        // GET: Reparacoes
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Admin"))
            {
                var reparacaoDB = await _context.Reparacoes
                                            .Include(r => r.Veiculo)
                                            .Include(r => r.FuncionariosEnvolvidosNaReparacao)
                                            .ToListAsync();

                return View(reparacaoDB);
            }
            else
            {

                // var. auxiliar
                string idDaPessoaAutenticada = _userManager.GetUserId(User);


                // quais as reparações que pertencem à pessoa que está autenticada?
                var reparacoes = await (from r in _context.Reparacoes.Include(r => r.Veiculo)
                                        join v in _context.Veiculos on r.IdVeiculo equals v.IdVeiculo
                                        join c in _context.Clientes on v.IdCliente equals c.IdCliente
                                        join u in _context.Users on c.Email equals u.Email
                                        where u.Id == idDaPessoaAutenticada
                                        select r)
                                 .ToListAsync();

                return View(reparacoes);

            }
        }

        // GET: Reparacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparacoes = await _context.Reparacoes
                .Include(r => r.Veiculo)
                .FirstOrDefaultAsync(r => r.IdReparacao == id);
            if (reparacoes == null)
            {
                return NotFound();
            }

            return View(reparacoes);
        }

        // GET: Reparacoes/Create
        [Authorize(Roles ="Cliente")]
        public async Task<IActionResult> CreateAsync()
        {

            // e, o ID fornecido pertence a um Veículo que pertence ao Utilizador que está a usar o sistema?
            int idClienteAutenticado = (await _context.Clientes.Where(c => c.UserName == _userManager.GetUserId(User)).FirstOrDefaultAsync()).IdCliente;

            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos.Include(v => v.Cliente).Where(v => v.IdCliente == idClienteAutenticado), "IdVeiculo", "Marca");
            return View();
        }

        // POST: Reparacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReparacao,IdVeiculo,TipoAvaria,DataRepar,Descricao,Estado")] Reparacoes reparacao)
        {
            
            if (ModelState.IsValid)
            {
                reparacao.Estado = "Em reparação";

                _context.Add(reparacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacao.IdVeiculo);
            return View(reparacao);
        }

        // GET: Reparacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparacoes = await _context.Reparacoes
                .Where(r => r.IdReparacao == id)
                .Include(r => r.FuncionariosEnvolvidosNaReparacao)
                .FirstOrDefaultAsync();

            if (reparacoes == null)
            {
                return NotFound();
            }

            ViewBag.ListaDeFuncionarios = await _context.Funcionarios.OrderBy(f => f.Nome).ToListAsync();

            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
            return View(reparacoes);
        }

        // POST: Reparacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("IdReparacao,IdVeiculo,TipoAvaria,DataRepar,Descricao,Estado")] Reparacoes newReparacao, int[] FuncionariosEscolhidos)
        {

            if (id != newReparacao.IdReparacao)
            {
                return NotFound();
            }

            // dados anteriormente guardados da Reparação
            var reparacao = await _context.Reparacoes.Where(r => r.IdReparacao == id).Include(r => r.FuncionariosEnvolvidosNaReparacao).FirstOrDefaultAsync();

            //**************************************************************************************************************************

            // obter a lista dos IDs dos Funcionários associadas às Reparações, antes da edição
            var oldListaFuncionarios = reparacao.FuncionariosEnvolvidosNaReparacao
                                           .Select(f => f.IdFuncionario)
                                           .ToList();

            // avaliar se o utilizador alterou alguma Funcionário associada à Reparação
            // adicionados -> lista de funcionários adicionados
            // retirados   -> lista de funcionários retirados
            var adicionados = FuncionariosEscolhidos.Except(oldListaFuncionarios);

            var retirados = oldListaFuncionarios.Except(FuncionariosEscolhidos.ToList());

            // se algum Funcionário foi adicionado ou retirado
            // é necessário alterar a lista de funcionário 
            // associada à Reparação
            if (adicionados.Any() || retirados.Any())
            {

                if (retirados.Any())
                {
                    // retirar o Funcionário 
                    foreach (int oldFuncionario in retirados)
                    {
                        var funcionarioARemover = reparacao.FuncionariosEnvolvidosNaReparacao.FirstOrDefault(f => f.IdFuncionario == oldFuncionario);
                        reparacao.FuncionariosEnvolvidosNaReparacao.Remove(funcionarioARemover);
                    }
                }
                if (adicionados.Any())
                {
                    // adicionar o Funcionário 
                    foreach (int newFuncionario in adicionados)
                    {
                        var FuncionarioAdd = await _context.Funcionarios.FirstOrDefaultAsync(f => f.IdFuncionario == newFuncionario);
                        reparacao.FuncionariosEnvolvidosNaReparacao.Add(FuncionarioAdd);
                    }
                }
            }
            //}

            //**************************************************************************************************************************

            if (ModelState.IsValid)
            {
                try
                {

                    /* a EF só permite a manipulação de um único objeto de um mesmo tipo
                  *  por esse motivo, como estamos a usar o objeto 'lesson'
                  *  temos de o atualizar com os dados que vêm da View
                  */
                    reparacao.Estado = newReparacao.Estado;

                    _context.Update(reparacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReparacoesExists(reparacao.IdReparacao))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacao.IdVeiculo);
            return View(reparacao);
        }

        // GET: Reparacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparacoes = await _context.Reparacoes
                .Include(r => r.Veiculo)
                .FirstOrDefaultAsync(m => m.IdReparacao == id);
            if (reparacoes == null)
            {
                return NotFound();
            }

            return View(reparacoes);
        }

        // POST: Reparacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reparacoes = await _context.Reparacoes.FindAsync(id);
            _context.Reparacoes.Remove(reparacoes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReparacoesExists(int id)
        {
            return _context.Reparacoes.Any(e => e.IdReparacao == id);
        }
    }
}
