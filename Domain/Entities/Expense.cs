using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }


        public ICollection<ConsumerExpense> ConsumerExpenses { get; set; }
    }
}
