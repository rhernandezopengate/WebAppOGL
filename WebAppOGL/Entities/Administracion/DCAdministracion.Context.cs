﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppOGL.Entities.Administracion
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_a3f19c_administracionEntities1 : DbContext
    {
        public db_a3f19c_administracionEntities1()
            : base("name=db_a3f19c_administracionEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<adm_area> adm_area { get; set; }
        public virtual DbSet<adm_sucursales> adm_sucursales { get; set; }
        public virtual DbSet<adm_cuentas> adm_cuentas { get; set; }
        public virtual DbSet<adm_puestos> adm_puestos { get; set; }
        public virtual DbSet<adm_empleados> adm_empleados { get; set; }

        public System.Data.Entity.DbSet<WebAppOGL.Entities.Sistemas.sis_asignacion_celulares> sis_asignacion_celulares { get; set; }
    }
}
