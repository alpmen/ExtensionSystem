using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Consumer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        public ICollection<ConsumerExpense> ConsumerExpenses { get; set; }
    }
}
