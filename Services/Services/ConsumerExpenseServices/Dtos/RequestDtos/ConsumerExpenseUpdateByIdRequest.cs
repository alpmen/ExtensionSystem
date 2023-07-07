namespace Services.Services.ConsumerExpenseServices.Dtos.RequestDtos
{
    public class ConsumerExpenseUpdateByIdRequest
    {
        public int ExpenseId { get; set; }
        public int ConsumerId { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public bool Status { get; set; }
    }
}