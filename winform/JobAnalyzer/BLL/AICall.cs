using System.Diagnostics;
using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace JobAnalyzer.BLL;

public partial class AICall : IDisposable
{
    public class AICallSettings
    {
        public string BaseUrl { get; set; } = "http://10.1.20.61:3100";
        public string ApiKey { get; set; } = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImQzMjkzNmZkLTdmNGMtNDU3ZS04YjBkLWZmZGQ4OWUyMzliNyIsImV4cCI6MTc3NDk5NjcwOCwianRpIjoiNTI5MTkwMDMtOWQwMy00OGQ3LWIxYjktNjgxN2YyMDU3YWExIn0.DzoQ6k9ng5JNiqbPCPxLNfsdNr61fWYPqM0RImA86oA";
        public string Prompt { get; set; } = "Based on documents Am I good candidate for this job? return json object as follow where company is the name of job post company name , missing_requirements is the requirement that I am missing  otherwise empy array '[]' job_requirements is the job post requirement or skills otherwise empy array '[]', reason is why I am good candidate, coverletter generate cover letter for this job post, matched return boolean value true if I am a candiadate otherwisise false ,jobtitle is the job post title{'company': '','missing_requirements': [],'job_requirements': [], 'reason': '', 'coverletter': '','matched': true or false, 'jobtitle': '' }";
        public string Model { get; set; } = "gpt-oss:20b";
    }

    AICallSettings settings = new AICallSettings();
    private bool disposedValue;

    public AICall(AICallSettings _settings)
    {
        this.settings = _settings;
    }

    public AICall()
    {
        this.settings = new AICall.AICallSettings();
    }

    internal async Task<string> GetJobsFolderIdAsync()
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {
                var response = await restClient
                    .GetAsync(new RestRequest("/api/v1/folders/") { Authenticator = new JwtAuthenticator(this.settings.ApiKey) });

                if (!response.IsSuccessful)
                {
                    response.ThrowIfError();
                }

                dynamic data = JsonConvert.DeserializeObject(response.Content);
                foreach (JObject item in data)
                {
                    if (item["name"] != null && item["name"].ToString().Contains("jobs", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return item["id"].ToString();
                    }
                }

                return string.Empty;
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }

    internal async Task<string> UploadFileAsync(string jobfilepath)
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {

                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest("/api/v1/files/?process=true&process_in_background=true")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };
                request.AddFile("file", jobfilepath);

                var response = restClient.PostAsync(request).Result;

                if (!response.IsSuccessful)
                {
                    response.ThrowIfError();
                }

                dynamic data = JsonConvert.DeserializeObject(response.Content);

                if (data["id"] != null)
                {
                    return data["id"].ToString();
                }


                return string.Empty;
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }

    internal async Task<string> UploadFileAsync(string filename, byte[] fileBytes)
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {

                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest("/api/v1/files/?process=true&process_in_background=true")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };
                request.AddFile("file", fileBytes, filename);

                var response = restClient.PostAsync(request).Result;

                if (!response.IsSuccessful)
                {
                    response.ThrowIfError();
                }

                dynamic data = JsonConvert.DeserializeObject(response.Content);

                if (data["id"] != null)
                {
                    return data["id"].ToString();
                }


                return string.Empty;
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }

    internal async Task<string> CreateChatAsync(string jobpostfile, string folderId, string resumeFileId, string jobpostid)
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {
                string prompt = this.settings.Prompt;
                string model = this.settings.Model;

                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest("/api/v1/chats/new")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };

                var payload = new
                {
                    chat = new
                    {
                        title = jobpostfile,
                        folder_id = folderId,
                        chat = new
                        {
                            models = new[] { model },
                            messages = new[]
                            {
                                new {
                                    role = "user",
                                    content = prompt,
                                    files = new[]
                                    {
                                        new { type = "file", id = resumeFileId },
                                        new { type = "file", id = jobpostid }
                                    }
                                }
                            },
                            options = new { }
                        }
                    }
                };

                request.AddBody(payload);

                var response = await restClient.PostAsync(request);

                if (!response.IsSuccessful)
                {
                    response.ThrowIfError();
                }

                dynamic data = JsonConvert.DeserializeObject(response.Content);

                if (data["id"] != null)
                {
                    return data["id"].ToString();
                }


                return string.Empty;
            }
            catch (Exception exp)
            {
                //Utilities.lo
                throw;
            }
        }
    }

    internal async Task<CheckFileStatusResponse> CheckFileStatusAsync(string fileId)
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {

                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest($"/api/v1/files/{fileId}")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };

                var response = await restClient.GetAsync(request);

                if (!response.IsSuccessful)
                {
                    response.ThrowIfError();
                }

                var methodResult = JsonConvert.DeserializeObject<CheckFileStatusResponse>(response.Content);

                while (true)
                {
                    System.Threading.Thread.Sleep(500);
                    if (methodResult.data.status.Contains("complete", StringComparison.InvariantCultureIgnoreCase))
                        break;
                    else
                        methodResult = await CheckFileStatusAsync(fileId);


                }

                return methodResult;
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }

    internal async Task<CompletionResponse> GetCompletionAsync(string chatId, string jobPostFileId, string resumeFileId)
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {
                string prompt = this.settings.Prompt;
                string model = this.settings.Model;

                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest("/api/chat/completions")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };

                var payload = new
                {
                    chatid = chatId,
                    model = model,
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = prompt
                        }
                    },
                                    files = new[]
                                    {
                        new { type = "file", id = jobPostFileId },
                        new { type = "file", id = resumeFileId }
                    }
                };

                request.AddBody(payload);
                request.Timeout = new TimeSpan(0, 5, 0);

                var response = await restClient.PostAsync(request);

                //if (!response.IsSuccessful)
                //{
                //    response.ThrowIfError();
                //}

                //dynamic data = JsonConvert.DeserializeObject(response.Content);

                //if (data["id"] != null)
                //{
                //    return data["id"].ToString();
                //}
                                
                return JsonConvert.DeserializeObject<CompletionResponse>(response.Content);
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }

    public async Task<AIResponse?> GetResponseAsync(string selectedFile)
    {
        CompletionResponse completionResponse = new CompletionResponse();
        try
        {
            using (AICall aicall = new AICall())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var folderId = await aicall.GetJobsFolderIdAsync();                
                FileInfo file = new FileInfo(selectedFile);
                //Uplod Jobfile
                string jobPostFileId = await aicall.UploadFileAsync(selectedFile);
                var statusJobPostFileId = await aicall.CheckFileStatusAsync(jobPostFileId);

                //Upload Resume file
                string resumeFileId = await aicall.UploadFileAsync("f_resume.pdf");
                var statusResumeFileId = await aicall.CheckFileStatusAsync(resumeFileId);

                //Create a Chat with these two files 
                string chatId = await aicall.CreateChatAsync(file.FullName, folderId, resumeFileId, jobPostFileId);

                completionResponse = await aicall.GetCompletionAsync(chatId, jobPostFileId, resumeFileId);

                sw.Stop();

                var ellapsed = sw.ElapsedMilliseconds;

                AIResponse aiResponse;
                try
                {
                    var content = completionResponse.choices[0].message.content.Cleanup();

                    aiResponse = JsonConvert.DeserializeObject<AIResponse>(content);
                    Utilities.Logger.Information($"deserialized content: {content} is {ellapsed} ms");
                    File.WriteAllText(selectedFile.Replace(".html", ".json"), JsonConvert.SerializeObject(aiResponse));
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                    throw exp;
                }             
                return aiResponse;
            }
        }
        catch (Exception exp)
        {
            File.WriteAllText(selectedFile.Replace(".html", ".json"), JsonConvert.SerializeObject(completionResponse));
            Utilities.Logger.Error(exp.Message);
            return null;
        }
    }

    public async Task<AIResponse?> GetResponseAsync(JobObject job)
    {
        CompletionResponse completionResponse = new CompletionResponse();
        try
        {
            using (AICall aicall = new AICall())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var folderId = await aicall.GetJobsFolderIdAsync();
                FileInfo file = new FileInfo(job.HTMLFileName);
                //Uplod Jobfile
                string jobPostFileId = await aicall.UploadFileAsync(job.TextFileName, job.TextFileContent);
                var statusJobPostFileId = await aicall.CheckFileStatusAsync(jobPostFileId);

                //Upload Resume file
                string resumeFileId = await aicall.UploadFileAsync("resume.txt");
                var statusResumeFileId = await aicall.CheckFileStatusAsync(resumeFileId);

                //Create a Chat with these two files 
                string chatId = await aicall.CreateChatAsync(file.FullName, folderId, resumeFileId, jobPostFileId);

                completionResponse = await aicall.GetCompletionAsync(chatId, jobPostFileId, resumeFileId);

                sw.Stop();

                var ellapsed = sw.ElapsedMilliseconds;

                AIResponse aiResponse;
                try
                {
                    var content = completionResponse.choices[0].message.content.Cleanup();


                    aiResponse = JsonConvert.DeserializeObject<AIResponse>(content);
                    Utilities.Logger.Information($"deserialized content: {content} is {ellapsed} ms");
                    File.WriteAllText(job.JsonFileName, JsonConvert.SerializeObject(aiResponse));
                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                    throw exp;
                }
                return aiResponse;
            }
        }
        catch (Exception exp)
        {
            File.WriteAllText(job.JsonFileName, JsonConvert.SerializeObject(completionResponse));
            Utilities.Logger.Error(exp.Message);
            return null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)                 
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~AICall()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
