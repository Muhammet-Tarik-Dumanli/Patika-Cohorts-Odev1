using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RestApiProject.DbOperations
{
    public class DataGenerator
    {
    
    /// <summary>
    /// Initializes the database with sample data.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
            using (var context = new CarDbContext(serviceProvider.GetRequiredService<DbContextOptions<CarDbContext>>()))
            {
                if (context.Cars.Any())
                {
                    return;
                }

                context.Cars.AddRange(
                    new Car
                    {
                        Brand = "Mercedes - Benz",
                        Model = "G63 4*4 Square",
                        Year = 2023,
                        IsAutomatic = true,
                    },
                    new Car
                    {
                        Brand = "Ford",
                        Model = "Granada",
                        Year = 1967,
                        IsAutomatic = false,
                    },
                    new Car
                    {
                        Brand = "Opel",
                        Model = "Corsa",
                        Year = 1997,
                        IsAutomatic = false,
                    },
                    new Car
                    {
                        Brand = "BMW",
                        Model = "M5 CS",
                        Year = 2020,
                        IsAutomatic = true,
                    },
                    new Car
                    {
                        Brand = "Mercedes - Benz",
                        Model = "C180",
                        Year = 2010,
                        IsAutomatic = true,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}