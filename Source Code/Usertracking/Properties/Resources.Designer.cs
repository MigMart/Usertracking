﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Usertracking.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Usertracking.Properties.Resources", typeof(Resources).Assembly);
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
        
        internal static System.Drawing.Bitmap backgoundgrid {
            get {
                object obj = ResourceManager.GetObject("backgoundgrid", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;grammar version=&quot;1.0&quot; xml:lang=&quot;en-US&quot; tag-format=&quot;semantics/1.0-literals&quot; xmlns=&quot;http://www.w3.org/2001/06/grammar&quot;&gt;
        ///  &lt;rule id=&quot;commands&quot; scope=&quot;public&quot;&gt;
        ///    &lt;one-of&gt;
        ///        &lt;item&gt;double click&lt;/item&gt;
        ///        &lt;item&gt;click&lt;/item&gt;
        ///        &lt;item&gt;click down&lt;/item&gt;
        ///        &lt;item&gt;click up&lt;/item&gt;
        ///        &lt;item&gt;left button&lt;/item&gt;
        ///        &lt;item&gt;right button&lt;/item&gt;
        ///        &lt;item&gt;drag&lt;/item&gt;
        ///        &lt;item&gt;drop&lt;/item&gt;
        ///    &lt;/one-of&gt;
        ///  &lt;/rule&gt;
        ///&lt;/grammar&gt;.
        /// </summary>
        internal static string Grammar {
            get {
                return ResourceManager.GetString("Grammar", resourceCulture);
            }
        }
        
        internal static byte[] Hand {
            get {
                object obj = ResourceManager.GetObject("Hand", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static System.Drawing.Bitmap Kinect {
            get {
                object obj = ResourceManager.GetObject("Kinect", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static byte[] PointUp {
            get {
                object obj = ResourceManager.GetObject("PointUp", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] Select {
            get {
                object obj = ResourceManager.GetObject("Select", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
