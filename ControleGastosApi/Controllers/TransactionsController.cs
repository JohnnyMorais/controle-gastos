using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleGastosApi.Data;
using ControleGastosApi.Models;

namespace ControleGastosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de todas as transações registradas no sistema.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        /// <summary>
        /// Registra uma nova transação financeira vinculada a uma pessoa.
        /// </summary>
        /// <param name="transaction">Dados da transação (descrição, valor, tipo e ID da pessoa).</param>
        /// <returns>Retorna a transação criada ou um erro caso as regras de negócio não sejam atendidas.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            // Validação de Integridade: Verifica se a pessoa informada existe no banco de dados
            var person = await _context.People.FindAsync(transaction.PersonId);
            if (person == null)
            {
                return BadRequest("O identificador da pessoa informado não existe no cadastro.");
            }

            // Regra de Negócio: Impede que menores de 18 anos registrem receitas
            // Conforme especificação: "Caso a pessoa informada seja menor de idade, apenas despesas poderão ser cadastradas."
            if (person.Age < 18 && transaction.Type == TransactionType.Receita)
            {
                return BadRequest("Usuários menores de 18 anos possuem restrição: apenas despesas podem ser cadastradas.");
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, transaction);
        }
    }
}