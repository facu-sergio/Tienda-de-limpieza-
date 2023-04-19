using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductosConMarcayTipo : BaseSpecification<Producto>
    {
        public ProductosConMarcayTipo(ProductosSpecParams productoParams)
            : base (x =>
                     (string.IsNullOrEmpty(productoParams.Search)|| x.Nombre.ToLower().Contains(productoParams.Search)) &&
                     (!productoParams.MarcaId.HasValue || x.MarcaId == productoParams.MarcaId ) &&
                     (!productoParams.TipoId.HasValue || x.TipoId == productoParams.TipoId)
                  )
        {
            AddInclude(x => x.Marca);
            AddInclude(x => x.Tipo);
            AddOrderBy(x => x.Nombre);
            ApplyPaging(productoParams.PageIndex > 0 ? productoParams.PageSize * (productoParams.PageIndex - 1) : 0, productoParams.PageSize);
            if (!string.IsNullOrEmpty(productoParams.Sort))
            {
                switch (productoParams.Sort)
                {
                    case "precioAsc":
                        AddOrderBy(p => p.Precio);
                        break;
                    case "precioDesc":
                        AddOrderByDescending(p => p.Precio);
                        break;
                    default:
                        AddOrderBy(n => n.Nombre);
                        break;
                }
            }
        }

        public ProductosConMarcayTipo(int id)
            : base(x=> x.Id == id)
        {
            AddInclude(x => x.Marca);
            AddInclude(x => x.Tipo);
        }

        
    }
}
