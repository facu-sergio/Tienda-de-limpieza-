using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ProductosConMarcayTipo : BaseSpecification<Producto>
    {
        public ProductosConMarcayTipo()
        {
            AddInclude(x => x.Marca);
            AddInclude(x => x.Tipo);
        }
    }
}
