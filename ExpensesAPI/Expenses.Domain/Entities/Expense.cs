namespace Expenses.Domain.Entities
{
    public class Expense: EntityBase
    {
        public int ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }

    }
}
