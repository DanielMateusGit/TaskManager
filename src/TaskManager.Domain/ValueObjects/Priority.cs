namespace TaskManager.Domain.ValueObjects;

public class Priority
{
    // This is a smart enum class
    public static readonly Priority Low = new Priority("Low", 1);
    public static readonly Priority Medium = new Priority("Medium", 2);
    public static readonly Priority High = new Priority("High", 3);
    public static readonly Priority Critical = new Priority("Critical", 4);
    
    // Internal state
    public string Name { get; }  
    public int Value { get; }
    
    // Private constructor
    private Priority(string name, int value) 
    {
        this.Name = name;
        this.Value = value;
    }
}