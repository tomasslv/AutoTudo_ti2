using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var reparacaoDB = _context.Reparacoes.Include(r => r.Veiculo);
            return View(await reparacaoDB.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca");
            return View();
        }

        // POST: Reparacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReparacao,IdVeiculo,TipoAvaria,DataRepar,Descricao,Estado")] Reparacoes reparacoes)
        {
            if (ModelState.IsValid)
            {

                // obter os veículos registados
                //Veiculos veiculo = _context.Veiculos.Where(v => v.IdVeiculo == _context.Veiculos).FirstOrDefault();
                // adicionar o veículo à reparação
                //reparacoes.Veiculo = veiculo;

                _context.Add(reparacoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
            return View(reparacoes);
        }

        // GET: Reparacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparacoes = await _context.Reparacoes.FindAsync(id);
            if (reparacoes == null)
            {
                return NotFound();
            }
            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
            return View(reparacoes);
        }

        // POST: Reparacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReparacao,IdVeiculo,TipoAvaria,DataRepar,Descricao,Estado")] Reparacoes reparacoes)
        {
            if (id != reparacoes.IdReparacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reparacoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReparacoesExists(reparacoes.IdReparacao))
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
            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
            return View(reparacoes);
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


//***********************************************************************************


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using StandV_ti2.Data;
//using StandV_ti2.Models;

//namespace StandV_ti2.Controllers
//{
//    [Authorize] // esta 'anotação' garante que só as pessoas autenticadas têm acesso aos recursos
//    public class ReparacoesController : Controller
//    {
//        /// <summary>
//        /// este atributo representa a base de dados do projeto
//        /// </summary>
//        private readonly ReparacaoDB _context;

//        /// <summary>
//        /// esta variável recolhe os dados da pessoa q se autenticou
//        /// </summary>
//        private readonly UserManager<ApplicationUser> _userManager;

//        public ReparacoesController(ReparacaoDB context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // GET: Reparacoes
//        public async Task<IActionResult> Index()
//        {
//            /* criação de uma variável que vai conter um conjunto de dados
//            * vindos da base de dados
//            * se fosse em SQL, a pesquisa seria:
//            *     SELECT *
//            *     FROM Fotografias f, Caes c, Criadores cr, CriadoresCaes cc
//            *     WHERE f.CaoFK = c.Id AND
//            *           cc.CaoFK = c.Id AND
//            *           cc.CriadorFK = cr.Id AND
//            *           cr.UserName = ID da pessoa que se autenticou
//            *  exatamente equivalente a _context.Fotografias.Include(f => f.Cao), feita em LINQ
//            *  f => f.Cao  <---- expressão 'lambda'
//            *  ^ ^  ^
//            *  | |  |
//            *  | |  representa cada um dos registos individuais da tabela das Fotografias
//            *  | |  e associa a cada fotografia o seu respetivo Cão
//            *  | |  equivalente à parte WHERE do comando SQL
//            *  | |
//            *  | um símbolo que separa os ramos da expressão
//            *  |
//            *  representa todos registos das fotografias
//            */
//            //var fotografias = _context.Fotografias
//            //                          .Include(f => f.Cao)
//            //                          .ThenInclude(c => c.ListaCriadores)
//            //                          .ThenInclude(cc => cc.Criador);
//            //                         // .Where(cr=>cr.UserName == _userManager.GetUserId(User));


//            // dados de todas os veículos
//            //var dadosveiculos = await _context.Veiculos.Include(f => f.Cliente).ToListAsync();

//            // var. auxiliar
//            string idDaPessoaAutenticada = _userManager.GetUserId(User);

//            // quais os veiculos que pertencem à pessoa que está autenticada?
//            // quais os seus IDs?
//            var veiculos = await (from c in _context.Veiculos
//                                  join cc in _context.Clientes on c.IdCliente equals cc.IdCliente
//                                  where cc.UserName == idDaPessoaAutenticada
//                                  select c.IdCliente)
//                             .ToListAsync();

//            // invoca a View, entregando-lhe a lista de registos dos veiculos
//            return View(veiculos);
//        }

//        // GET: Reparacoes/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                // entro aqui se não foi especificado o ID

//                // redirecionar para a página de início
//                return RedirectToAction("Index");

//                //return NotFound();
//            }

//            var reparacoes = await _context.Reparacoes
//                .Include(r => r.Gestor)
//                .Include(r => r.Veiculo)
//                .FirstOrDefaultAsync(m => m.IdReparacao == id);
//            if (reparacoes == null)
//            {
//                // o ID especificado não corresponde a uma fotografia

//                // return NotFound();
//                // redirecionar para a página de início
//                return RedirectToAction("Index");
//            }

//            return View(reparacoes);
//        }

//        // GET: Reparacoes/Create
//        public IActionResult Create()
//        {

//            /* geração da lista de valores disponíveis na DropDown
//            * o ViewData transporta dados a serem associados ao atributo 'CaoFK'
//            * o SelectList é um tipo de dados especial que serve para armazenar a lista 
//            * de opções de um objeto do tipo <SELECT> do HTML
//            * Contém dois valores: ID + nome a ser apresentado no ecrã
//            * 
//            * _context.Caes : representa a fonte dos dados
//            *                 na prática estamos a executar o comando SQL
//            *                 SELECT * FROM Caes
//            * 
//            * vamos alterar a pesquisa para significar
//            * SELECT * FROM Caes ORDER BY Nome
//            * e, a minha expressão fica: _context.Caes.OrderBy(c=>c.Nome)
//            * 
//            */

//            // _context.Veiculos.OrderBy(c => c.Marca)  -> obtem a lista de todos os Veiculos
//            // mas, queremos apenas a lista de cães do utilizador autenticado
//            var veiculos = (from c in _context.Reparacoes
//                            join cc in _context.Veiculos on c.IdVeiculo equals cc.IdVeiculo
//                            join cr in _context.Clientes on cc.IdVeiculo equals cr.IdCliente
//                            where cr.UserName == _userManager.GetUserId(User)
//                            select c)
//                       .OrderBy(c => c.IdVeiculo);

//            ViewData["IdVeiculo"] = new SelectList(veiculos, "Id", "Marca");


//            return View();
//        }

//        // POST: Reparacoes/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("IdReparacao,IdVeiculo,IdGestor,TipoAvaria,DataRepar,Descricao,Estado")] Reparacoes reparacoes)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(reparacoes);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
//            return View(reparacoes);
//        }

//        // GET: Reparacoes/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var reparacoes = await _context.Reparacoes.FindAsync(id);
//            if (reparacoes == null)
//            {
//                return NotFound();
//            }
//            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
//            return View(reparacoes);
//        }

//        // POST: Reparacoes/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("IdReparacao,IdVeiculo,IdGestor,TipoAvaria,DataRepar,Descricao,Estado")] Reparacoes reparacoes)
//        {
//            if (id != reparacoes.IdReparacao)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(reparacoes);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ReparacoesExists(reparacoes.IdReparacao))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdGestor"] = new SelectList(_context.Gestores, "IdGestor", "CodPostal", reparacoes.IdGestor);
//            ViewData["IdVeiculo"] = new SelectList(_context.Veiculos, "IdVeiculo", "Marca", reparacoes.IdVeiculo);
//            return View(reparacoes);
//        }

//        // GET: Reparacoes/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var reparacoes = await _context.Reparacoes
//                .Include(r => r.Gestor)
//                .Include(r => r.Veiculo)
//                .FirstOrDefaultAsync(m => m.IdReparacao == id);
//            if (reparacoes == null)
//            {
//                return NotFound();
//            }

//            return View(reparacoes);
//        }

//        // POST: Reparacoes/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var reparacoes = await _context.Reparacoes.FindAsync(id);
//            _context.Reparacoes.Remove(reparacoes);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ReparacoesExists(int id)
//        {
//            return _context.Reparacoes.Any(e => e.IdReparacao == id);
//        }
//    }
//}
