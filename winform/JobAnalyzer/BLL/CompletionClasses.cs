using Newtonsoft.Json;

namespace JobAnalyzer.BLL;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class CompletionChoice
{
    public int index { get; set; }
    public object logprobs { get; set; }
    public string finish_reason { get; set; }
    public CompletionMessage message { get; set; }
}

public class CompletionTokensDetails
{
    public int reasoning_tokens { get; set; }
    public int accepted_prediction_tokens { get; set; }
    public int rejected_prediction_tokens { get; set; }
}

public class CompletionMessage
{
    public string role { get; set; }
    public string content { get; set; }
    public string reasoning_content { get; set; }
}

public class CompletionMetadata
{
    public string hash { get; set; }
    public string source { get; set; }
    public int start_index { get; set; }
    public string created_by { get; set; }
    public string name { get; set; }
    public string embedding_config { get; set; }
    public string title { get; set; }
    public string file_id { get; set; }
    public int? page { get; set; }
    public DateTime? creationdate { get; set; }
    public string author { get; set; }
    public string page_label { get; set; }
    public string producer { get; set; }
    public int? total_pages { get; set; }
    public DateTime? moddate { get; set; }
    public string creator { get; set; }
}

public class CompletionResponse
{
    public List<CompletionSource> sources { get; set; }
    public string id { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public List<CompletionChoice> choices { get; set; }
    public string @object { get; set; }
    public Usage usage { get; set; }
}

public class CompletionSource
{
    public CompletionSource source { get; set; }
    public List<string> document { get; set; }
    public List<CompletionMetadata> metadata { get; set; }
    public List<double> distances { get; set; }
}

public class Source2
{
    public string type { get; set; }
    public string id { get; set; }
}

public class Usage
{
    public int input_tokens { get; set; }
    public int output_tokens { get; set; }
    public int total_tokens { get; set; }
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }

    [JsonProperty("response_token/s")]
    public double response_tokens { get; set; }

    [JsonProperty("prompt_token/s")]
    
    public long total_duration { get; set; }
    public int load_duration { get; set; }
    public int prompt_eval_count { get; set; }
    public long prompt_eval_duration { get; set; }
    public int eval_count { get; set; }
    public long eval_duration { get; set; }
    public string approximate_total { get; set; }
    public CompletionTokensDetails completion_tokens_details { get; set; }
}


public class JobResponse
{
    public string company { get; set; }
    public List<string> missing_requirements { get; set; }
    public List<string> job_requirements { get; set; }
    public string reason { get; set; }
    public string coverletter { get; set; }
    public bool matched { get; set; }
    public string jobtitle { get; set; }
}
