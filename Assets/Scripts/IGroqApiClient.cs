using System.Threading.Tasks;

namespace GroqApiLibrary
{
    public interface IGroqApiClient
    {
        Task<string> CreateChatCompletionAsync(string prompt);
    }
    
}
