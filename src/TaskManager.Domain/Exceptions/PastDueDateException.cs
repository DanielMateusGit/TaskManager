namespace TaskManager.Domain.Exceptions;                                                                                                       
                                                                                                                                                 
public class PastDueDateException : ArgumentException                                                                                          
{                                                                                                                                              
    public PastDueDateException() : base("Due date cannot be in the past") {}                                                                  
}