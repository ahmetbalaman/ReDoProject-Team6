//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReDoProject.Shared.Resources.Helpers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    ///   This class was generated by MSBuild using the GenerateResource task.
    ///   To add or remove a member, edit your .resx file then rerun MSBuild.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Build.Tasks.StronglyTypedResourceBuilder", "15.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CommonTranslations_en_GB {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonTranslations_en_GB() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ReDoProject.Shared.Resources.Helpers.CommonTranslations.en-GB", typeof(CommonTranslations_en_GB).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Passwords.
        /// </summary>
        internal static string Passwords {
            get {
                return ResourceManager.GetString("Passwords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome.
        /// </summary>
        internal static string WelcomeMessage {
            get {
                return ResourceManager.GetString("WelcomeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to our Blazor application.
        /// </summary>
        internal static string WelcomeMessageBlazor {
            get {
                return ResourceManager.GetString("WelcomeMessageBlazor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome Page.
        /// </summary>
        internal static string WelcomePage {
            get {
                return ResourceManager.GetString("WelcomePage", resourceCulture);
            }
        }
    }
}