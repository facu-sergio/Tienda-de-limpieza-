using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductosConFiltrosParaEspecificaciónDeConteo : BaseSpecification<Producto>
    {
        public ProductosConFiltrosParaEspecificaciónDeConteo(ProductosSpecParams productoParams)
            : base(x =>
                     (string.IsNullOrEmpty(productoParams.Search)|| x.Nombre.ToLower().Contains(productoParams.Search)) &&
                     (!productoParams.MarcaId.HasValue || x.MarcaId == productoParams.MarcaId ) &&
                     (!productoParams.TipoId.HasValue || x.TipoId == productoParams.TipoId)
                  )
        {

        }
    }
}
