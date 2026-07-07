using System.ComponentModel.DataAnnotations;

namespace ControleGastosApi.Models
{
    /// <summary>
    /// Representa uma transação financeira (Receita ou Despesa) vinculada a uma pessoa.
    /// </summary>
    public class Transaction
    {
        /// <summary>Identificador único da transação.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Descrição detalhada sobre a origem ou destino do valor.</summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>Valor monetário da transação.</summary>
        [Required]
        public decimal Value { get; set; }

        /// <summary>Define se a transação é uma 'Receita' ou 'Despesa'.</summary>
        [Required]
        public TransactionType Type { get; set; }

        /// <summary>Chave estrangeira que vincula esta transação a uma pessoa específica.</summary>
        [Required]
        public int PersonId { get; set; }

        /// <summary>
        /// Propriedade de navegação que permite ao Entity Framework acessar os dados da Pessoa proprietária desta transação.
        /// </summary>
        public Person? Person { get; set; }
    }
}