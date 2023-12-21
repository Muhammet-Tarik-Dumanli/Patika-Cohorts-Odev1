using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestApiProject;
using RestApiProject.DbOperations;

namespace CarController.AddControllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class CarController : ControllerBase
    {
        private readonly CarDbContext _context;

        public CarController(CarDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Returns a list of all cars in the database, sorted by their ID.
        /// </summary>
        /// <returns>A list of cars.</returns>
        [HttpGet]
        public List<Car> GetCars()
        {
            var carList = _context.Cars.OrderBy(car => car.Id).ToList<Car>();
            return carList;
        }

        /// <summary>
        /// Returns a list of cars filtered by the given name.
        /// </summary>
        /// <param name="name">The name to filter by.</param>
        /// <returns>A list of cars filtered by the given name.</returns>
        [HttpGet("list")]
        public IActionResult GetCars([FromQuery]string name)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(car => car.Brand.Contains(name));
            
            if(query.Count() > 0)
            {
                var carList = query.OrderBy(car => car.Id).ToList();
                return Ok(carList);
            }
            
            return BadRequest();
        }

        /// <summary>
        /// Returns a specific car based on its ID.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>The requested car, or null if no car with the given ID exists.</returns>
        [HttpGet("{id}")]
        public Car GetById(int id)
        {
            var car = _context.Cars.Where(car => car.Id == id).SingleOrDefault();
            return car;
        }
        
        // [HttpGet]
        // public Car GetFromQuery([FromQuery]string id)
        // {
        //     var car = _context.Cars.Where(car => car.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return car;
        // }

        /// <summary>
        /// Adds a new car to the database.
        /// </summary>
        /// <param name="newCar">The new car to add.</param>
        /// <returns>A status code indicating whether the operation was successful.</returns>
        [HttpPost]
        public IActionResult AddCar([FromBody] Car newCar)
        {
            var car = _context.Cars.SingleOrDefault(car => car.Id == newCar.Id);

            if (car != null)
                return BadRequest();

            //BookList.Add(newBook);
            _context.Cars.Add(newCar);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Updates a car with the specified ID in the database.
        /// </summary>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="updatedCar">The updated car information.</param>
        /// <returns>A status code indicating whether the update operation was successful.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
        {
            //var book = BookList.SingleOrDefault(book => book.Id == updatedBook.Id);
            var car = _context.Cars.SingleOrDefault(book => book.Id == updatedCar.Id);

            if (car == null)
                return BadRequest();

            car.Brand = updatedCar.Brand != default ? updatedCar.Brand : car.Brand;
            car.Model = updatedCar.Model != default ? updatedCar.Model : car.Model;
            car.Year = updatedCar.Year != default ? updatedCar.Year : car.Year;
            car.IsAutomatic = updatedCar.IsAutomatic != default ? updatedCar.IsAutomatic : car.IsAutomatic;

            _context.SaveChanges();

            return Ok();

        }

        /// <summary>
        /// Deletes a car with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>A status code indicating whether the delete operation was successful.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(car => car.Id == id);

            if (car == null)
            {
                return BadRequest();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return Ok();
        }

    }
}