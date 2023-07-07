namespace Services.Services.ConsumerExpenseServices.Dtos.RequestDtos
{
    public class ConsumerExpenseInsertRequest
    {
        public int ExpenseId { get; set; }
        public int ConsumerId { get; set; }
        public int Cost { get; set; }
    }
}