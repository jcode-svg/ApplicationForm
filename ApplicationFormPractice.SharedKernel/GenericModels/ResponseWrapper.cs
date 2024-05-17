using System.Text.Json.Serialization;

namespace ApplicationFormPractice.SharedKernel.GenericModels;

public class ResponseWrapper
{
    public string Message { get; set; } = null!;
    public bool IsSuccessful { get; set; }
    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new List<string>();
    public string Title { get; set; } = string.Empty;
}

public class ResponseWrapper<T> : ResponseWrapper where T : class
{
    public T ResponseObject { get; set; } = null!;
    public bool ResponseObjectExists => ResponseObject != null;

    public static ResponseWrapper<T> Success(T instance, string message = "Successful") => new ResponseWrapper<T>()
    {
        ResponseObject = instance,
        Message = message,
        IsSuccessful = true
    };

    public static ResponseWrapper<T> Error(string error, string message = "", string title = "Oops!") => new ResponseWrapper<T>()
    {
        IsSuccessful = false,
        Message = string.IsNullOrWhiteSpace(message) ? error : message,
        Errors = new List<string> { error },
        Title = title
    };

    public static ResponseWrapper<T> ErrorList(List<string> errors, string message = "", string title = "Oops!") => new ResponseWrapper<T>()
    {
        IsSuccessful = false,
        Message = message,
        Errors = errors,
        Title = title
    };

    public static ResponseWrapper<T> NotFound(T instance = null, string message = "", string title = "Oops!") => new ResponseWrapper<T>()
    {
        IsSuccessful = false,
        Message = message,
        Errors = new List<string> { message },
        ResponseObject = instance,
        Title = title
    };
}
