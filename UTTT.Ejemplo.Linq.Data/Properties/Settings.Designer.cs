﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UTTT.Ejemplo.Linq.Data.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        
        //[global::System.Configuration.ApplicationScopedSettingAttribute()]
        //[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //[global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        //[global::System.Configuration.DefaultSettingValueAttribute("Data Source=DESKTOP-TMRB37A\\SQLEXPRESS;Initial Catalog=Persona;Persist Security I" +
        //    "nfo=True;User ID=sa;Password=280619")]
        //public string PersonaConnectionString1 {
        //    get {
        //        return ((string)(this["PersonaConnectionString1"]));
        //    }
        //}
        
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=DBPersona3.mssql.somee.com;Initial Catalog=DBPersona3;User ID=DanielP" +
            "erez_SQLLogin_4;Password=1cablwmiwf")]
        public string DBPersona3ConnectionString {
            get {
                return ((string)(this["DBPersona3ConnectionString"]));
            }
        }
    }
}
