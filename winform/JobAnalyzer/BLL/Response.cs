using System.Text.Json.Serialization;

namespace BLL;

public class Response<T>
{
    public Response(T result)
    {
        Result = result;
    }

    public Response(Exception exp)
    {
        Exception = exp;
    }

    public T Result { get; set; }
    [JsonIgnore]
    private Exception Exception { get; set; }
    public string? ErrorMessage => Exception?.Message;
    public bool Success => string.IsNullOrEmpty(ErrorMessage);
}

