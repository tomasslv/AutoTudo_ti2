﻿using System;
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
    [Authorize] // esta 'anotação' garante que só as pessoas autenticadas têm acesso aos recursos
    public class VeiculosController : Controller
    {
        /// <summary>
        /// este atributo representa a base de dados do projeto
        /// </summary>
        private readonly ReparacaoDB _context;

        /// <summary>
        /// esta variável recolhe os dados da pessoa q se autenticou
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// este atributo representa a base de dados do projeto
        /// </summary>
        public VeiculosController(ReparacaoDB context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
            //var reparacaoDB = _context.Veiculos.Include(v => v.Cliente);
            //return View(await reparacaoDB.ToListAsync());

            // var. auxiliar
            string idDaPessoaAutenticada = _userManager.GetUserId(User);

            var veiculos = await (from v in _context.Veiculos.Include(v => v.Cliente)
                                  join c in _context.Clientes on v.IdCliente equals c.IdCliente
                                  join u in _context.Users on c.Email equals u.Email
                                  where u.Id == idDaPessoaAutenticada
                                  select v)
                                  .ToListAsync();

            return View(veiculos);
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // entro aqui se não foi especificado o ID

                // redirecionar para a página de início
                return RedirectToAction("Index");

                //return NotFound();
            }

            // se chego aqui, foi especificado um ID
            // vou procurar se existe um Veículo com esse valor
            var veiculos = await _context.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVeiculo == id);
            if (veiculos == null)
            {
                // o ID especificado não corresponde a um veículo

                // return NotFound();
                // redirecionar para a página de início
                return RedirectToAction("Index");
            }

            // se cheguei aqui, é pq o veículo existe e foi encontrado
            // então, mostro-o na View
            return View(veiculos);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "CodPostal");
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Marca,Modelo,AnoVeiculo,Combustivel,Matricula,Potencia,Cilindrada,Km,TipoConducao")] Veiculos veiculo)
        {
            if (ModelState.IsValid)
            {
                // obter os dados da pessoa autenticada
                Clientes cliente = _context.Clientes.Where(c => c.UserName == _userManager.GetUserId(User)).FirstOrDefault();
                // adicionar o cliente ao veículo
                veiculo.Cliente = cliente;

                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // e, o ID fornecido pertence a um Veículo que pertence ao Utilizador que está a usar o sistema?
            int idClienteAutenticado = (await _context.Clientes.Where(c => c.UserName == _userManager.GetUserId(User)).FirstOrDefaultAsync()).IdCliente;

            var veiculo = await _context.Veiculos.Where(v => v.IdVeiculo == id && v.IdCliente == idClienteAutenticado).FirstOrDefaultAsync();
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVeiculo,Marca,Modelo,AnoVeiculo,Combustivel,Matricula,Potencia,Cilindrada,Km,TipoConducao,IdCliente")] Veiculos veiculo)
        {
            if (id != veiculo.IdVeiculo)
            {
                return NotFound();
            }

            // recuperar o ID do utilizador (cliente) que está autenticado
            // e reassociar esse ID ao Veículo
            int idClienteAutenticado = (await _context.Clientes.Where(c => c.UserName == _userManager.GetUserId(User)).FirstOrDefaultAsync()).IdCliente;
            veiculo.IdCliente = idClienteAutenticado;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculosExists(veiculo.IdVeiculo))
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
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVeiculo == id);
            if (veiculos == null)
            {
                return NotFound();
            }

            return View(veiculos);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculos = await _context.Veiculos.FindAsync(id);
            _context.Veiculos.Remove(veiculos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculosExists(int id)
        {
            return _context.Veiculos.Any(e => e.IdVeiculo == id);
        }
    }
}
