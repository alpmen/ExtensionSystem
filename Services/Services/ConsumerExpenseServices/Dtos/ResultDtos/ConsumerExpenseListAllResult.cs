namespace Services.Services.ConsumerExpenseServices.Dtos.ResultDtos
{
    public class ConsumerExpenseListAllResult
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ConsumerId { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public bool Status { get; set; }
    }
}
