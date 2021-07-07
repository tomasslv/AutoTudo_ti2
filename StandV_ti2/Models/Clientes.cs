using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StandV_ti2.Models
{
    public class Clientes
    {
        public Clientes()
        {
            ListaVeiculos = new HashSet<Veiculos>();
        }


        /// <summary>
        /// Identificador do Cliente
        /// </summary>
        [Key]
        public int IdCliente { get; set; }

        /// <summary>
        /// Nome do Funciorario
        /// </summary>
        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "O Nome não pode ter mais de 60 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Morada do Funcionario
        /// </summary>
        [Required(ErrorMessage = "A Morada é de preenchimento obrigatório")]
        [StringLength(60, ErrorMessage = "A morada não pode ter mais de 60 caracteres.")]
        public string Morada { get; set; }

        /// <summary>
        /// Código Postal do Cliente
        /// </summary>
        [Required(ErrorMessage = "Deve escrever o código postal")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "O código postal deve ter entre 30 e 8 caracteres.")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        /// <summary>
        /// Numero de telemóvel do Funcionario
        /// </summary>
        [StringLength(14, MinimumLength = 9, ErrorMessage = "O número deve ter entre 9 e 14 caracteres.")]
        [RegularExpression("(00)?([0-9]{2,3})?[1-9][0-9]{8}", ErrorMessage = "Escreva, por favor, um nº de telemóvel com 9 algarismos. Se quiser, pode acrescentar o indicativo nacional e o internacional.")]
        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }

        /// <summary>
        /// Email do Funcionario
        /// </summary>
        [StringLength(50, ErrorMessage = "O email não pode ter mais de 50 caracteres.")]
        [EmailAddress(ErrorMessage = "O email introduzido não é válido.")]
        [RegularExpression("[a-z]+(([a-z]|[0-9])*)@gmail.com",
                           ErrorMessage = "Só são aceites emails da google.")]
        public string Email { get; set; }

        /// <summary>
        /// Numero de cartão de cidadão do Funcionario
        /// </summary>
        [Required(ErrorMessage = "Deve escrever o Número de Identificação Fiscal.")]
        [StringLength(9, ErrorMessage = "O número deve conter {1} caracteres.")]
        [RegularExpression("[1-9]+[0-9]{8}", ErrorMessage = "Escreva, por favor, um nº de cartão de cidadão válido.")]
        [Display(Name = "Cartão de Cidadão")]
        public string NIF { get; set; }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        public ICollection<Veiculos> ListaVeiculos { get; set; }

        //************************************************************************************
        /// <summary>
        /// Funciona como Chave Forasteira no relacionamento entre os Clientes
        /// e a tabela de autenticação
        /// </summary>
        public string UserName { get; set; }

        //************************************************************************************
    }
}
