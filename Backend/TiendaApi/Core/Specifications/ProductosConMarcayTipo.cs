using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductosConMarcayTipo : BaseSpecification<Producto>
    {
        public ProductosConMarcayTipo(string sort, int? marcaId, int?TipoId)
            : base (x=>
                     (!marcaId.HasValue || x.MarcaId == marcaId ) &&
                     (!TipoId.HasValue || x.TipoId == TipoId)
                  )
        {
            AddInclude(x => x.Marca);
            AddInclude(x => x.Tipo);
            AddOrderBy(x => x.Nombre);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
