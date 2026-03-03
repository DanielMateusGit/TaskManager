using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.ValueObjects;

public class DueDate
{
    public DateTime Value {get; }
    public bool IsOverdue => this.Value < DateTime.Now; 

    private DueDate(DateTime dateTime)
    {
        if (dateTime < DateTime.Today) throw new PastDueDateException();
        this.Value = dateTime;
    }
    public static DueDate FromDateTime(DateTime dateTime) => new DueDate(dateTime);
    public int DaysRemaining() => this.Value.Subtract(DateTime.Now).Days + 1;
}