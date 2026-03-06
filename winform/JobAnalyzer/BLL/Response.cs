using System.Text.Json.Serialization;

namespace JobAnalyzer.BLL;

public class Response<T> : Response
{
    public Response(T result)
    {
        Result = result;
    }

    public Response(Exception exp)
    {
        this.Exception = exp;
    }

    public T Result { get; set; }
}

public class Response
{
    public Response()
    {
    }

    public Response(Exception exp)
    {
        Exception = exp;
    }
    [JsonIgnore]
    public Exception Exception { get; internal set; }
    public string? ErrorMessage => Exception?.Message;
    public bool Success => string.IsNullOrEmpty(ErrorMessage);
}

