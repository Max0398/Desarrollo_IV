﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ProyectoApi.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Conductors = new HashSet<Conductor>();
            DatosUsuarios = new HashSet<DatosUsuario>();
            Departamentos = new HashSet<Departamento>();
            Destinos = new HashSet<Destino>();
            Municipios = new HashSet<Municipio>();
            Retornos = new HashSet<Retorno>();
            Salida = new HashSet<Salida>();
            TipoViajes = new HashSet<TipoViaje>();
            Unidads = new HashSet<Unidad>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int IdTipoUsuario { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Conductor> Conductors { get; set; }
        public virtual ICollection<DatosUsuario> DatosUsuarios { get; set; }
        public virtual ICollection<Departamento> Departamentos { get; set; }
        public virtual ICollection<Destino> Destinos { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; }
        public virtual ICollection<Retorno> Retornos { get; set; }
        public virtual ICollection<Salida> Salida { get; set; }
        public virtual ICollection<TipoViaje> TipoViajes { get; set; }
        public virtual ICollection<Unidad> Unidads { get; set; }
    }
}