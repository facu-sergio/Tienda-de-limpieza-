using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        

        public static async Task SeedAsync(TiendaContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Marcas.Any())
                {
                    var marcasData =   File.ReadAllText("../Infrastructure/Data/SeedData/Marcas.json");
                    var marcas = JsonSerializer.Deserialize<List<Marca>>(marcasData);

                    foreach(var marca in marcas)
                    {
                        context.Marcas.Add(marca);
                    }
                    await context.SaveChangesAsync();   

                }
                if (!context.Tipos.Any())
                {
                    var tiposData = File.ReadAllText("../Infrastructure/Data/SeedData/Tipos.json");
                    var tipos = JsonSerializer.Deserialize<List<Tipo>>(tiposData);

                    foreach (var tipo in tipos)
                    {
                        context.Tipos.Add(tipo);
                    }
                    await context.SaveChangesAsync();

                }
                if (!context.Productos.Any())
                {
                    var productosData = File.ReadAllText("../Infrastructure/Data/SeedData/Productos.json");
                    var productos= JsonSerializer.Deserialize<List<Producto>>(productosData);

                    foreach (var producto in productos)
                    {
                        context.Productos.Add(producto);
                    }
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
