﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestForm.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Stegosaurus.HuffmanTable HuffmanTableYDC {
            get {
                return ((global::Stegosaurus.HuffmanTable)(this["HuffmanTableYDC"]));
            }
            set {
                this["HuffmanTableYDC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Stegosaurus.HuffmanTable HuffmanTableChrAC {
            get {
                return ((global::Stegosaurus.HuffmanTable)(this["HuffmanTableChrAC"]));
            }
            set {
                this["HuffmanTableChrAC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Stegosaurus.HuffmanTable HuffmanTableChrDC {
            get {
                return ((global::Stegosaurus.HuffmanTable)(this["HuffmanTableChrDC"]));
            }
            set {
                this["HuffmanTableChrDC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Stegosaurus.QuantizationTable QuantizationTableY {
            get {
                return ((global::Stegosaurus.QuantizationTable)(this["QuantizationTableY"]));
            }
            set {
                this["QuantizationTableY"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Stegosaurus.QuantizationTable QuantizationTableChr {
            get {
                return ((global::Stegosaurus.QuantizationTable)(this["QuantizationTableChr"]));
            }
            set {
                this["QuantizationTableChr"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("53")]
        public int QualityGT {
            get {
                return ((int)(this["QualityGT"]));
            }
            set {
                this["QualityGT"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool QualityGTLocked {
            get {
                return ((bool)(this["QualityGTLocked"]));
            }
            set {
                this["QualityGTLocked"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ImagesFilePath {
            get {
                return ((string)(this["ImagesFilePath"]));
            }
            set {
                this["ImagesFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Stegosaurus.HuffmanTable HuffmanTableYAC {
            get {
                return ((global::Stegosaurus.HuffmanTable)(this["HuffmanTableYAC"]));
            }
            set {
                this["HuffmanTableYAC"] = value;
            }
        }
    }
}
