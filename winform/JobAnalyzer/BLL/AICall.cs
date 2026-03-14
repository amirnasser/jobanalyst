using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Extensions.Configuration;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace JobAnalyzer.BLL;

public partial class AICall : IDisposable
{
    AICallSettings settings;
    private bool disposedValue;

    public AICall(AICallSettings _settings)
    {
        this.settings = _settings;
    }

    public AICall()
    {
        this.settings = LoadFromAppSetting();
    }

    private async Task<string> GetJobsFolderIdAsync()
    {
        var options = new RestClientOptions(this.settings.BaseUrl)
        {
            Authenticator = new JwtAuthenticator(this.settings.ApiKey)
        };

        using (var restClient = new RestClient(options))
        {
            try
            {
                var response = await restClient.GetAsync(new RestRequest("/api/v1/folders/"));

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

    private async Task<string> UploadFileAsync(string filename)
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
                request.AddFile("file", filename, filename);

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

    private async Task<string> UploadFileAsync(string filename, byte[] fileBytes)
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

    private async Task<string> CreateChatAsync(string jobpostfile, string folderId, string resumeFileId, string jobpostid)
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
                throw;
            }
        }
    }

    private async Task<CheckFileStatusResponse> CheckFileStatusAsync(string fileId)
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

    private async Task<CompletionResponse> GetCompletionAsync(string chatId, string jobPostFileId, string resumeFileId)
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

                return JsonConvert.DeserializeObject<CompletionResponse>(response.Content);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }

    private async Task<AIResponse?> GetResponseAsync(string selectedFile)
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
                string jobPostFileId = await aicall.UploadFileAsync(selectedFile, File.ReadAllBytes(selectedFile));
                var statusJobPostFileId = await aicall.CheckFileStatusAsync(jobPostFileId);

                //Upload Resume file
                string resumeFileId = await aicall.UploadFileAsync("resume.txt", File.ReadAllBytes("resume.txt"));
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
            Utilities.Logger.Error(exp.Message);
            return null;
        }
    }

    public async Task<Response<JobObject>> UploadAndProcess(string folder, JobObject job)
    {
        CompletionResponse completionResponse = new CompletionResponse();
        try
        {
            using (AICall aicall = new AICall())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var folderId = await aicall.GetJobsFolderIdAsync();
                FileInfo file = new FileInfo(Path.Combine(folder, job.DataFileName));
                //Uplod Jobfile
                string jobPostFileId = await aicall.UploadFileAsync(job.DataFileName, Encoding.UTF8.GetBytes(job.Text));
                var statusJobPostFileId = await aicall.CheckFileStatusAsync(jobPostFileId);

                //Upload Resume file
                string resumeFileId = await aicall.UploadFileAsync("resume.txt", File.ReadAllBytes("resume.txt"));
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

                    if(aiResponse != null)
                    {
                        job.Matched = aiResponse.matched;
                        job.Coverletter = aiResponse.coverletter;
                        job.MissingRequirements = aiResponse.missing_requirements.ToList();
                        job.JobRequirements = aiResponse.job_requirements.ToList();
                        job.Reason = aiResponse.reason;
                        job.Title = aiResponse.jobtitle;
                        job.ProfessionalSummary = aiResponse.professional_summary;
                        job.SuggestedResume = aiResponse.suggested_resume;
                    }

                    return new Response<JobObject>(job);

                }
                catch (Exception exp)
                {
                    Utilities.Logger.Error(exp.Message);
                    return new Response<JobObject>(job);
                }
                
            }
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp.Message);
            return new Response<JobObject>(job);
        }
    }

    private AICallSettings LoadFromAppSetting()
    {
        var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var settings = new AICallSettings();
        config.GetSection("AICallSettings").Bind(settings);
        return settings;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
