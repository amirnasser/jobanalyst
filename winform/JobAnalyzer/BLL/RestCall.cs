using Newtonsoft.Json;
using RestSharp;


namespace JobAnalyzer.BLL;

public class RestCall
{
    public static async Task<ApiVersion?> GetVersion(string apiBaseUrl = "http://localhost:5000")
    {
        //add basic authentication to call

        var options = new RestClientOptions(apiBaseUrl)
        {
            Timeout = new TimeSpan(0, 5, 0),
            ThrowOnAnyError = false
            //Authenticator = new HttpBasicAuthenticator("jobapi", "Amir2647@")
        };
        var client = new RestClient(options);


        var request = new RestRequest("/", Method.Get);
        request.AlwaysMultipartFormData = true;
        try
        {
            RestResponse response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiVersion>(response.Content);
        }
        catch (Exception exp)
        {
            return null;
        }


    }

    public static async Task<AIResponse?> Analyze(string jobPost, string myResume = "f_resume.pdf", string apiBaseUrl = "http://localhost:5000")
    {
        var options = new RestClientOptions(apiBaseUrl)
        {
            Timeout = new TimeSpan(0, 5, 0),
            ThrowOnAnyError = false
        };
        var client = new RestClient(options);


        var request = new RestRequest("/analyze", Method.Post);
        request.AlwaysMultipartFormData = true;
        request.AddFile("file", jobPost);
        request.AddFile("resume", myResume);
        RestResponse response = await client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<AIResponse>(response.Content);
    }
}
