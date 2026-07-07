namespace ControleGastosApi.Models
{
    /// <summary>
    /// Define os tipos permitidos para as transações financeiras.
    /// Utilizado para validar se uma transação é de entrada (Receita) ou saída (Despesa).
    /// </summary>
    public enum TransactionType
    {
        /// <summary>Representa uma saída de recursos financeiros.</summary>
        Despesa = 0,

        /// <summary>Representa uma entrada de recursos financeiros.</summary>
        Receita = 1
    }
}