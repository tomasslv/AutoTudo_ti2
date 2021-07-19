using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandV_ti2.Data;
using StandV_ti2.Models;

namespace StandV_ti2.Controllers.API
{
    [Route("API/[controller]")]
    [ApiController]
    public class VeiculosAPI : ControllerBase
    {
        private readonly  ReparacaoDB _context;

        private readonly IWebHostEnvironment _caminho;

        public VeiculosAPI(ReparacaoDB context, IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;
        }

        // GET: api/VeiculosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculosAPIViewModel>>> GetVeiculos()
        {
            var listaVeiculos = await _context.Veiculos
                .Select(v => new VeiculosAPIViewModel
                {
                    IdVeiculo = v.IdVeiculo,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    AnoVeiculo = v.AnoVeiculo,
                    Combustivel = v.Combustivel,
                    Matricula = v.Matricula,
                    Potencia = v.Potencia,
                    Cilindrada = v.Cilindrada,
                    Km = v.Km,
                    TipoConducao = v.TipoConducao
                })
                .OrderBy(f => f.IdVeiculo)
                .ToListAsync();
            return listaVeiculos;
        }

        // GET: API/VeiculosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculos>> GetVeiculos(int id)
        {
            var veiculos = await _context.Veiculos.FindAsync(id);

            if (veiculos == null)
            {
                return NotFound();
            }

            return veiculos;
        }

        // PUT: API/VeiculosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculos(int id, Veiculos veiculo)
        {
            if (id != veiculo.IdVeiculo)
            {
                return BadRequest();
            }

            _context.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: API/VeiculosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Veiculos>> PostVeiculos([FromForm] Veiculos veiculo)
        {

            veiculo.IdCliente = 3;
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVeiculos", new { id = veiculo.IdVeiculo }, veiculo);
        }

        // DELETE: API/VeiculosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculos(int id)
        {
            var veiculos = await _context.Veiculos.FindAsync(id);
            if (veiculos == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(veiculos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeiculosExists(int id)
        {
            return _context.Veiculos.Any(v => v.IdVeiculo == id);
        }
    }
}