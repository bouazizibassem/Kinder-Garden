#region assembly System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\System.Web.dll
#endregion

using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Web
{
    //
    // Résumé :
    //     Sert de classe de base pour les classes qui permettent à ASP.NET de lire les
    //     valeurs HTTP envoyées par un client lors d'une requête web.
    [DefaultMember("Item")]
    [TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
    public abstract class HttpRequestBase
    {
        public virtual Uri Url { get; }
    }
}