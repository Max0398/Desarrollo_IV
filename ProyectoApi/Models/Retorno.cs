﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ProyectoApi.Models
{
    public partial class Retorno
    {
        public int IdRetorno { get; set; }
        public DateTime? Fhretorno { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public int IdDestino { get; set; }

        public virtual Destino IdDestinoNavigation { get; set; }
        public virtual Usuario IdUsuarioRegistroNavigation { get; set; }
    }
}