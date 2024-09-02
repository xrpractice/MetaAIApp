using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GroqApiLibrary;
using Newtonsoft.Json;
using UnityEngine;

public class GroqApiClient : IGroqApiClient
{
    private readonly HttpClient client;
    

    public GroqApiClient(string apiKey)
    {
        client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    public async Task<string> CreateChatCompletionAsync(string prompt)
    {
        StringContent httpContent = new StringContent("{\"model\": \"llama3-8b-8192\",\"messages\": [{\"role\": \"user\",\"content\": \"" + prompt + "\"}]}"
            , Encoding.UTF8,
            "application/json");
        using var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", httpContent);
        response.EnsureSuccessStatusCode();
        
        var body = await response.Content.ReadAsStringAsync();
         Response data = JsonConvert.DeserializeObject<Response>(body);
         Debug.Log(body);
         return data.choices[0].message.content;
    }
}

public class Response
{
    public Choice[] choices;
}

public class Choice
{
    public Message message;
}
    
public class Message
{
    public string role;
    public string content;
}
