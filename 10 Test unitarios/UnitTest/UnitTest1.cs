using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Controllers;
using TestAPI.Models;

namespace UnitTest
{
    public class UnitTest1
    {
        private readonly PeopleController _controller;
        private readonly TestDbContext _context;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new TestDbContext(options);
            _controller = new PeopleController(_context);
        }
        [Fact]
        public async Task CreaPersonaOk()
        {
            var newPerson = new Person
            {
                Name = "Ana",
                Age = 24
            };

            var result = await _controller.PostPerson(newPerson);

            var personInDb = await _context.Persons.FindAsync(newPerson.Id);
            Assert.NotNull(personInDb);
            Assert.Equal("Ana", personInDb.Name);
            Assert.Equal(24, personInDb.Age);
        }

        [Fact]
        public async Task GetPersonsLista()
        {
            // Arrange: Agregar datos de prueba
            _context.Persons.Add(new Person { Name = "John", Age = 30 });
            _context.Persons.Add(new Person { Name = "Jane", Age = 25 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetPersons();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Person>>>(result);
            var persons = Assert.IsAssignableFrom<IEnumerable<Person>>(okResult.Value);
            Assert.Equal(2, persons.Count());
        }

        [Fact]
        public async Task PersonaNoExiste()
        {
            // Arrange
            var id = 999;

            // Act
            var result = await _controller.GetPerson(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task ActualizarPersonaOk()
        {
            // Arrange
            var newPerson = new Person
            {
                Name = "Ana",
                Age = 24
            };
            await _context.Persons.AddAsync(newPerson);
            await _context.SaveChangesAsync();

            newPerson.Name = "Ana Elisa";

            // Act
            var result = await _controller.PutPerson(newPerson.Id, newPerson);

            // Assert
            var okResult = Assert.IsType<NoContentResult>(result);
            var personInDb = await _context.Persons.FindAsync(newPerson.Id);
            Assert.Equal("Ana Elisa", personInDb.Name);
        }
    }
}