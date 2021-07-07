using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StandV_ti2.Models
{
    public class Veiculos
    {
        public Veiculos()
        {
            ListaReparacoes = new HashSet<Reparacoes>();
        }

        /// <summary>
        /// Identificador do Veículo
        /// </summary>
        [Key]
        public int IdVeiculo { get; set; }

        /// <summary>
        /// Marca do Veículo
        /// </summary>
        [Required(ErrorMessage = "A marca do Veículo é de preenchimento obrigatório")]
        public string Marca { get; set; }

        /// <summary>
        /// Modelo do Veículo
        /// </summary>
        [Required(ErrorMessage = "O modelo do Veículo é de preenchimento obrigatório")]
        public string Modelo { get; set; }

        /// <summary>
        /// Ano do Veículo
        /// </summary>
        [Display(Name = "Ano do Veículo")]
        public DateTime AnoVeiculo { get; set; }

        /// <summary>
        /// Tipo de combustivel do Veículo
        /// </summary>
        [Display(Name = "Combustível")]
        public string Combustivel { get; set; }

        /// <summary>
        /// Matrícula do Veículo
        /// </summary>
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        /// <summary>
        /// Potencia do Veículo
        /// </summary>
        [Display(Name = "Potência")]
        public string Potencia { get; set; }

        /// <summary>
        /// Potencia do Veículo
        /// </summary>
        [Display(Name = "Cilindrada")]
        public string Cilindrada { get; set; }

        /// <summary>
        /// Numero de Kms do Veículo
        /// </summary>
        [Display(Name = "Nº de Kms do Veículo")]
        public int Km { get; set; }

        /// <summary>
        /// Tipo de condução do Veículo (automatico/manual)
        /// </summary>
        [Display(Name = "Tipo de condução do Veículo")]
        public int TipoConducao { get; set; }

        //********************************************************************************

        /// <summary>
        /// FK para identificar o CLiente
        /// </summary>
        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }
        public Clientes Cliente { get; set; }

        //********************************************************************************

        public ICollection<Reparacoes> ListaReparacoes { get; set; }
    }
}
