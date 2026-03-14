using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using static JobAnalyzer.BLL.AICall;

namespace JobAnalyzer.BLL;

public class HtmlCleanerAndBeautifier
{
    AICallSettings settings = new AICallSettings();
    public HtmlCleanerAndBeautifier()
    {
        this.settings = new AICallSettings();
    }
    
    internal string getPrompt(string html)
    {
        string prompt = @"
You are an expert HTML refactoring engine. Clean up the following HTML while preserving only meaningful semantic content.

### Your goals:
1. Remove all unnecessary, duplicated, or auto‑generated elements:
   - Delete all <div>, <span>, <section>, <article>, <p>, <ul>, <li>, <svg>, and <img> elements that exist only for styling, tracking, layout, or analytics.
   - Remove all classes, IDs, data-* attributes, ARIA attributes, tracking attributes, and inline styles.
   - Remove all empty tags and redundant wrappers.

2. Rebuild the HTML using a clean, minimal, semantic structure:
   - Use only semantic tags: <header>, <main>, <section>, <h1>–<h4>, <p>, <ul>, <li>, <a>, <footer>.
   - Convert deeply nested structures into flat, readable sections.
   - Preserve text content exactly as written.

3. Apply a simple, modern, responsive layout:
   - Wrap the entire content in a single <main> container.
   - Use a full‑width responsive layout with a max-width container (e.g., class=""container"" or no class if simpler).
   - Do NOT include external CSS frameworks.
   - Add a minimal internal <style> block with:
       - max-width: 900px;
       - margin: auto;
       - padding: 20px;
       - responsive typography.

4. Output requirements:
   - Produce valid, clean HTML5.
   - No JavaScript.
   - No comments.
   - No placeholder text.
   - Do not invent or rewrite content—only clean and restructure it.

### Input HTML:
```html
" + html + @"
```    
### Output:
```html
Return only the cleaned, simplified and beautify semantic HTML here.
```    
";
        return prompt;
    }

    internal async Task CleanupAsync(string html, string model = "gpt-oss:20b")
    {
        html = html.Trim().Replace("\n", "").Replace(" ", "");
        using (RestSharp.RestClient restClient = new RestClient(this.settings.BaseUrl))
        {
            // using rest charp create a chat with open-webui rest api
            try
            {


                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest("/api/v1/chats/new")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };

                var payload = new
                {
                    chat = new
                    {
                        title = "Clean up html",
                        chat = new
                        {
                            models = new[] { model },
                            messages = new[]
                            {
                                new {
                                    role = "user",
                                    content = getPrompt(html),
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
                    var a = data["id"].ToString();
                }
            }
            catch (Exception exp)
            {
                //Utilities.lo
                throw;
            }


            // Implement your HTML cleanup and beautification logic here
            // For example, you can use HtmlAgilityPack or any other library to parse and clean the HTML
            // This is a placeholder implementation that simply returns the original content
            //return htmlContent;
        }
    }

    internal async Task<CompletionResponse> GetCompletionAsync(string html, string model = "gpt-oss:20b")
    {
        using (RestSharp.RestClient restClient = new RestSharp.RestClient(this.settings.BaseUrl))
        {
            try
            {                

                CancellationToken cancellationToken = new CancellationToken();
                var request = new RestRequest("/api/chat/completions")
                {
                    Authenticator = new JwtAuthenticator(this.settings.ApiKey),
                };

                var payload = new
                {
                    chatid = "b880a1d3-44b9-4a93-a9a3-99e28128b580",
                    model = model,
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = getPrompt(html)
                        }
                    }
                };

                request.AddBody(payload);
                request.Timeout = new TimeSpan(0, 5, 0);

                var response = await restClient.PostAsync(request);

                return JsonConvert.DeserializeObject<CompletionResponse>(response.Content);
            }
            catch (Exception exp)
            {
                throw;
            }
        }
    }

    public async Task<string> CleanupAndBeautifyAsync(string html)
    {
        //create chatend get completion
        //await CleanupAsync(html);
        var completionResponse = await GetCompletionAsync(html);
        try
        {
            var content = completionResponse.choices[0].message.content.Cleanup();
            return content;
        }
        catch (Exception exp)
        {
            Utilities.Logger.Error(exp.Message);
            throw exp;
        }
    }
}