using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ConsumerExpense
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ConsumerId { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public bool Status { get; set; }


        public virtual Consumer Consumer { get; set; }
        public virtual Expense Expense { get; set; }
    }
}
