﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Security.Application.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SecurityResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SecurityResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Security.Application.Localization.SecurityResource", typeof(SecurityResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to العنوان.
        /// </summary>
        public static string Address {
            get {
                return ResourceManager.GetString("Address", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to المدينة.
        /// </summary>
        public static string CityId {
            get {
                return ResourceManager.GetString("CityId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تأكيد كلمة المرور.
        /// </summary>
        public static string ConfirmPassword {
            get {
                return ResourceManager.GetString("ConfirmPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تأكيد كلمة المرور لا يتطابق مع كلمة المرور..
        /// </summary>
        public static string ConfirmPasswordInvalid {
            get {
                return ResourceManager.GetString("ConfirmPasswordInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الدولة.
        /// </summary>
        public static string CountryId {
            get {
                return ResourceManager.GetString("CountryId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الحي.
        /// </summary>
        public static string DistrictId {
            get {
                return ResourceManager.GetString("DistrictId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to البريد الإلكتروني.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الإسم.
        /// </summary>
        public static string FullName {
            get {
                return ResourceManager.GetString("FullName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to البريد الإلكتروني غير صحيح.
        /// </summary>
        public static string InvalidEmail {
            get {
                return ResourceManager.GetString("InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الجوال.
        /// </summary>
        public static string Mobile {
            get {
                return ResourceManager.GetString("Mobile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to كلمة المرور.
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to المنطقة.
        /// </summary>
        public static string RegionId {
            get {
                return ResourceManager.GetString("RegionId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to الصلاحيات.
        /// </summary>
        public static string Roles {
            get {
                return ResourceManager.GetString("Roles", resourceCulture);
            }
        }
    }
}