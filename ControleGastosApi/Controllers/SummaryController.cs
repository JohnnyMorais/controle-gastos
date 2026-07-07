using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using ControleGastosApi.Data;

using ControleGastosApi.Models;
 
namespace ControleGastosApi.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class SummaryController : ControllerBase

    {

        private readonly AppDbContext _context;
 
        public SummaryController(AppDbContext context)

        {

            _context = context;

        }
 
        [HttpGet]

        public async Task<IActionResult> GetTotals()

        {

            // R.P. - Carrega as pessoas trazendo junto suas respectivas transações

            var people = await _context.People.Include(p => p.Transactions).ToListAsync();
 
            var summary = people.Select(p => {

                var receitas = p.Transactions.Where(t => t.Type == TransactionType.Receita).Sum(t => t.Value);

                var despesas = p.Transactions.Where(t => t.Type == TransactionType.Despesa).Sum(t => t.Value);

                return new {

                    p.Id,

                    p.Name,

                    TotalReceitas = receitas,

                    TotalDespesas = despesas,

                    Saldo = receitas - despesas

                };

            }).ToList();
 
            // Cálculos agregados para o fechamento do relatório geral

            var totalGeralReceitas = summary.Sum(s => s.TotalReceitas);

            var totalGeralDespesas = summary.Sum(s => s.TotalDespesas);

            var saldoLiquidoGeral = totalGeralReceitas - totalGeralDespesas;
 
            return Ok(new {

                Pessoas = summary,

                TotalGeral = new {

                    TotalReceitas = totalGeralReceitas,

                    TotalDespesas = totalGeralDespesas,

                    SaldoLiquido = saldoLiquidoGeral

                }

            });

        }

    }

}
 