//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Encuestas
{
    using System;
    using System.Collections.Generic;
    
    public partial class detalleEncuestas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public detalleEncuestas()
        {
            this.respuestasEncuestas = new HashSet<respuestasEncuestas>();
        }
    
        public int idDetalleEncuesta { get; set; }
        public string nombreCampo { get; set; }
        public string titulo { get; set; }
        public bool esRequerido { get; set; }
        public string tipoCampo { get; set; }
        public int idEncuesta { get; set; }
    
        public virtual encuestas encuestas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<respuestasEncuestas> respuestasEncuestas { get; set; }
    }
}
