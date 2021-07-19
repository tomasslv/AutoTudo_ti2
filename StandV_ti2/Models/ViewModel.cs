using System;
using System.Collections.Generic;

namespace StandV_ti2.Models
{

    /// <summary>
    /// lista os dados dos veículos a serem disponibilizados na API
    /// </summary>
    public class VeiculosAPIViewModel
    {
        /// <summary>
        /// Identificador do Veículo
        /// </summary>
        public int IdVeiculo { get; set; }

        /// <summary>
        /// Marca do Veículo
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Modelo do Veículo
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Ano do Veículo
        /// </summary>
        public DateTime AnoVeiculo { get; set; }

        /// <summary>
        /// Tipo de combustivel do Veículo
        /// </summary>
        public string Combustivel { get; set; }

        /// <summary>
        /// Matrícula do Veículo
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Potencia do Veículo
        /// </summary>
        public string Potencia { get; set; }

        /// <summary>
        /// Potencia do Veículo
        /// </summary>
        public string Cilindrada { get; set; }

        /// <summary>
        /// Numero de Kms do Veículo
        /// </summary>
        public int Km { get; set; }

        /// <summary>
        /// Tipo de condução do Veículo (automatico/manual)
        /// </summary>
        public string  TipoConducao { get; set; }
    }


}