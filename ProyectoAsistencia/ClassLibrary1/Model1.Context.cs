﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GestionAcademicaEntities : DbContext
    {
        public GestionAcademicaEntities()
            : base("name=GestionAcademicaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administradores> Administradores { get; set; }
        public virtual DbSet<Aprendices> Aprendices { get; set; }
        public virtual DbSet<Asistencias> Asistencias { get; set; }
        public virtual DbSet<Codigos_Asistencia> Codigos_Asistencia { get; set; }
        public virtual DbSet<Competencias> Competencias { get; set; }
        public virtual DbSet<Fichas> Fichas { get; set; }
        public virtual DbSet<Instructores> Instructores { get; set; }
        public virtual DbSet<Programas> Programas { get; set; }
        public virtual DbSet<Reportes_Incapacidad> Reportes_Incapacidad { get; set; }
        public virtual DbSet<Soportes_Asistencia> Soportes_Asistencia { get; set; }
    }
}