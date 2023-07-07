using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.ConsumerExpenseServices.Dtos.ResultDtos
{
    public class ConsumerExpenseGetByIdResult
    {
        public int ExpenseId { get; set; }
        public int ConsumerId { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public bool Status { get; set; }
    }
}
