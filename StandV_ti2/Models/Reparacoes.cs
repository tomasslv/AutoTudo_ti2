using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StandV_ti2.Models
{
    public class Reparacoes
    {
        public Reparacoes()
        {
            FuncionariosEnvolvidosNaReparacao = new HashSet<Funcionarios>();
        }

        /// <summary>
        /// Identificador da Reparação
        /// </summary>
        [Key]
        public int IdReparacao { get; set; }

        //###########################################################################

        /// <summary>
        /// lista de funcionário que participam na reparação de uma viatura
        /// </summary>
        public ICollection<Funcionarios> FuncionariosEnvolvidosNaReparacao { get; set; }

        /// <summary>
        /// Identificador do Veiculo que vai ser reparado
        /// </summary>
        [Display(Name = "Veículo")]
        [ForeignKey(nameof(Veiculo))]
        public int IdVeiculo { get; set; }
        public Veiculos Veiculo { get; set; }


        //##########################################################################

        /// <summary>
        /// Tipo de Avaria
        /// </summary>
        [Display(Name = "Reparação")]
        public string TipoAvaria { get; set; }

        /// <summary>
        /// Data da Reparação
        /// </summary>
        [Display(Name = "Data de pedido de reparação")]
        public DateTime DataRepar { get; set; }

        /// <summary>
        /// Peças para Reparação
        /// </summary>
        [StringLength(200, ErrorMessage = "Não pode ter mais de 200 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        /// <summary>
        /// Estado da Reparação ("Em reparação" ou "Concluído")
        /// </summary>
        [Display(Name = "Estado")]
        public string Estado { get; set; }

    }
}
