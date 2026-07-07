using System.ComponentModel.DataAnnotations;
using ControleGastosApi.Models;

namespace ControleGastosApi.Models
{
    /// <summary>
    /// Representa a entidade Pessoa no sistema.
    /// Contém os dados cadastrais básicos e a lista de transações vinculadas.
    /// </summary>
    public class Person
    {
        /// <summary>Identificador único gerado automaticamente pelo banco de dados.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Nome completo da pessoa.</summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>Idade da pessoa, utilizada para validar restrições de cadastro de receitas.</summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// Lista de transações associadas a esta pessoa. 
        /// O Entity Framework utiliza esta lista para manter a integridade do relacionamento 1:N.
        /// </summary>
        public List<Transaction> Transactions { get; set; } = new();
    }
}