using System;
using System.Diagnostics.CodeAnalysis;

namespace Player.Api.Exceptions;

[ExcludeFromCodeCoverage]
public class ExceptionMessage
{
    public ExceptionMessage(object? excObject, string methodName, string message)
    {
        ExceptionDateTime = DateTime.Now;
        ExcObject = excObject;
        MethodName = methodName;
        Message = message;
    }

    public DateTime ExceptionDateTime { get; set; }
    public object? ExcObject { get; set; }
    public string MethodName { get; set; }
    public string Message { get; set; }
}