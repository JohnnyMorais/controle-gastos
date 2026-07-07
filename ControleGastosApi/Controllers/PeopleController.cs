using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleGastosApi.Data;
using ControleGastosApi.Models;

namespace ControleGastosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as pessoas cadastradas no sistema.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        /// <summary>
        /// Adiciona uma nova pessoa ao cadastro.
        /// </summary>
        /// <param name="person">Objeto contendo Nome e Idade.</param>
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
        }

        /// <summary>
        /// Remove uma pessoa e todas as suas transações vinculadas.
        /// </summary>
        /// <param name="id">O identificador único da pessoa.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound caso a pessoa não exista.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null) return NotFound("Pessoa não encontrada.");

            // A exclusão em cascata (Cascade Delete) configurada no AppDbContext 
            // garante a integridade referencial, removendo automaticamente 
            // as transações associadas a este ID.
            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}