﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppOGL.Entities.Sistemas
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_a3f19c_administracionEntities : DbContext
    {
        public db_a3f19c_administracionEntities()
            : base("name=db_a3f19c_administracionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sis_mantenimiento> sis_mantenimiento { get; set; }
        public virtual DbSet<sis_marcas> sis_marcas { get; set; }
        public virtual DbSet<sis_tipoequipos> sis_tipoequipos { get; set; }
        public virtual DbSet<sis_estatusequipo> sis_estatusequipo { get; set; }
        public virtual DbSet<sis_equipos> sis_equipos { get; set; }
        public virtual DbSet<sis_celulares> sis_celulares { get; set; }
        public virtual DbSet<sis_asignacion> sis_asignacion { get; set; }
        public virtual DbSet<sis_asignacion_celulares> sis_asignacion_celulares { get; set; }
        public virtual DbSet<sis_terminales> sis_terminales { get; set; }
        public virtual DbSet<sis_asignacion_terminales> sis_asignacion_terminales { get; set; }
        public virtual DbSet<sis_statusfiscal> sis_statusfiscal { get; set; }
        public virtual DbSet<sis_tipoimpresoras> sis_tipoimpresoras { get; set; }
        public virtual DbSet<sis_asignacion_impresoras> sis_asignacion_impresoras { get; set; }
        public virtual DbSet<sis_impresoras> sis_impresoras { get; set; }
        public virtual DbSet<sis_telefonosip> sis_telefonosip { get; set; }
    }
}
