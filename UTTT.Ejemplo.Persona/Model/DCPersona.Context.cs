﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UTTT.Ejemplo.Persona.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBPersona3Entities : DbContext
    {
        public DBPersona3Entities()
            : base("name=DBPersona3Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CatEstadoCivil> CatEstadoCivil { get; set; }
        public virtual DbSet<CatSexo> CatSexo { get; set; }
        public virtual DbSet<CatUsuario> CatUsuario { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
