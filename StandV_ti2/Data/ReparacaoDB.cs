using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StandV_ti2.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace StandV_ti2.Data
{
    /// <summary>
    /// classe para recolher os dados particulares dos Utilizadores
    /// vamos deixar de usar o 'IdentityUser' e começar a usar este
    /// A adição desta classe implica:
    ///    - mudar a classe de criação da Base de Dados
    ///    - mudar no ficheiro 'startup.cs' a referência ao tipo do utilizador
    ///    - mudar em todos os ficheiros do projeto a referência a 'IdentityUser' 
    ///           para 'ApplicationUser'
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// recolhe a data de registo de um utilizador
        /// </summary>
        public DateTime DataRegisto { get; set; }

        // /// <summary>
        // /// se fizerem isto, estão a adicionar todos os atributos do 'Cliente'
        // /// à tabela de autenticação
        // /// </summary>
        // public Clientes Cliente { get; set; }
    }

    /// <summary>
    /// esta classe representa a Base de Dados a ser utilizada neste projeto
    /// </summary>
    public class ReparacaoDB : IdentityDbContext<ApplicationUser>
    {
        public ReparacaoDB(DbContextOptions<ReparacaoDB> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // insert DB seed

            modelBuilder.Entity<Funcionarios>().HasData(
               new Funcionarios { IdFuncionario = 1, Nome = "João Pedro Silva", CodPostal = "2300-675", Email = "joaopedro@gmail.com", Morada = "Rua Capelo e Ivens, Santarém", Telemovel = "915453329", Fotografia = "func02.jpg", Cargo = "Mecânico" },
               new Funcionarios { IdFuncionario = 2, Nome = "André Costa", CodPostal = "2070-116", Email = "andrecos@gmail.com", Morada = "Rua Serpa Pinto, Cartaxo", Telemovel = "917549939", Fotografia = "func03.png", Cargo = "Pintor" },
               new Funcionarios { IdFuncionario = 3, Nome = "Alberto Santos", CodPostal = "2543-322", Email = "albertosantos@gmail.com", Morada = "Rua João César Monteiro, Lisboa", Telemovel = "925142348", Fotografia = "func01.png", Cargo = "Bate-chapas" }
            );

        }

        //Representar as Tabelas da BD
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Reparacoes> Reparacoes { get; set; }
        public DbSet<Veiculos> Veiculos { get; set; }


    }
}

