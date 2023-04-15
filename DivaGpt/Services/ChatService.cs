using Azure.AI.OpenAI;

namespace DivaGpt.Services
{
    /// <summary>
    /// Chatで使うデータとか処理とかはここに集約させる
    /// </summary>
    public interface IChatService
    {
        public List<ChatMessage> DefaultPrompt { get; }
        public List<ChatMessage> Test { get; }
    }

    public class ChatService : IChatService
    {
        List<ChatMessage> IChatService.DefaultPrompt => defaultPrompt;
        private List<ChatMessage> defaultPrompt = new()
        {
        };
        
        List<ChatMessage> IChatService.Test => test;
        private List<ChatMessage> test = new()
        {
            new ChatMessage(ChatRole.System,
                "あなたはプログラマです。プログラミングの質問に答えてください。")  // 適当
        };
    }

}
