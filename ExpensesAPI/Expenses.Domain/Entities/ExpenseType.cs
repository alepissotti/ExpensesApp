namespace Expenses.Domain.Entities
{
    public class ExpenseType: EntityBase
    {
        public string Description { get; set; }
        public  IEnumerable<Expense> Expenses { get; set; }
    }
}
