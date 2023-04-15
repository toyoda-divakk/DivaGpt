using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace DivaGpt.Services
{
    /// <summary>
    /// チャットで使う画面とデータを管理する
    /// </summary>
    public interface IChatFormatService
    {
        public Dictionary<string, Display> PageData { get; }
        public bool IsDebugMode { get; }

        /// <summary>
        /// 有効なページの一覧を取得する
        /// </summary>
        /// <returns></returns>
        public List<Display> GetKeys();
    }

    public class ChatFormatService : IChatFormatService
    {
        private readonly ChatOption _options;
        IChatService _chat;

        public Dictionary<string, Display> PageData => _pageData;
        public Dictionary<string, Display> _pageData = new();
        public bool IsDebugMode => _options.IsLocalDevelopMode;

        public ChatFormatService(IChatService chat, IOptions<ChatOption> options)
        {
            _chat = chat;
            _options = options.Value;

            // 追加していくこと
            //_pageData.Add("Openai3", new Display
            //{
            //    Address = "Openai3",
            //    Title = "OpenAI:gpt-3.5-turbo",
            //    Headline = "OpenAI:gpt-3.5-turbo",
            //    Introduction = "安い。基本的にこれを使うこと。\n$0.002 / 1K tokens",
            //    Placeholder = "あなたの質問",
            //    MasterPrompt = _chat.DefaultPrompt,
            //    GptModel = "gpt-3.5-turbo",
            //    IsPublic = false
            //});
            //_pageData.Add("Openai4", new Display
            //{
            //    Address = "Openai4",
            //    Title = "OpenAI:gpt-4",
            //    Headline = "OpenAI:gpt-4",
            //    Introduction = "高い。基本的に使わないこと。\n8K context, プロンプト: $0.03 / 1K tokens, 補完: $0.06 / 1K tokens\n32K context, プロンプト: $0.06 / 1K tokens, 補完: $0.12 / 1K tokens",
            //    Placeholder = "あなたの質問",
            //    MasterPrompt = _chat.DefaultPrompt,
            //    GptModel = "gpt-4", // gpt-4, gpt-4-0314, gpt-4-32k, gpt-4-32k-0314（32kはまだ非公開）
            //    IsPublic = false,
            //    MaxTokens = 2048
            //});
            //_pageData.Add("Openai4-0314", new Display
            //{
            //    Address = "Openai4-0314",
            //    Title = "OpenAI:gpt-4-0314",
            //    Headline = "OpenAI:gpt-4-0314",
            //    Introduction = "高い。基本的に使わないこと。\n8K context, プロンプト: $0.03 / 1K tokens, 補完: $0.06 / 1K tokens\n32K context, プロンプト: $0.06 / 1K tokens, 補完: $0.12 / 1K tokens",
            //    Placeholder = "あなたの質問",
            //    MasterPrompt = _chat.DefaultPrompt,
            //    GptModel = "gpt-4-0314",
            //    IsPublic = false,
            //    MaxTokens = 2048
            //});
            _pageData.Add("Azure3", new Display
            {
                Address = "Azure3",
                Title = "Azure:gpt-35-turbo",
                Headline = "Azure OpenAI Service:gpt-35-turbo",
                Introduction = "会社用1。\n$0.002 / 1K tokens",
                Placeholder = "あなたの質問",
                MasterPrompt = _chat.DefaultPrompt,
                UseAzureOpenAI = true,
                GptModel = options.Value.Model1,
                IsPublic = false,
                MaxTokens = 2048
            });
            //_pageData.Add("AzureCode2", new Display
            //{
            //    Address = "AzureCode2",
            //    Title = "Azure:code-davinci-002",
            //    Headline = "Azure OpenAI Service:code-davinci-002",
            //    Introduction = "会社用2、コード生成用だけど割高。他とはUIが違うので要書き換え",
            //    Placeholder = "あなたの質問",
            //    MasterPrompt = _chat.DefaultPrompt,
            //    UseAzureOpenAI = true,
            //    GptModel = options.Value.Model4,
            //    IsPublic = false,
            //    IsCode = true,
            //    MaxTokens = 2048
            //});
            _pageData.Add("Azure4-8k", new Display
            {
                Address = "Azure4-8k",
                Title = "Azure:gpt-4",
                Headline = "Azure OpenAI Service:gpt-4",
                Introduction = "会社用2、高い。\n8K context, プロンプト: $0.03 / 1K tokens, 補完: $0.06 / 1K tokens",
                Placeholder = "あなたの質問",
                MasterPrompt = _chat.DefaultPrompt,
                UseAzureOpenAI = true,
                GptModel = options.Value.Model2,
                IsPublic = true,
                MaxTokens = 2048
            });
            _pageData.Add("Azure4-32k", new Display
            {
                Address = "Azure4-32k",
                Title = "Azure:gpt-4-32k",
                Headline = "Azure OpenAI Service:gpt-4",
                Introduction = "会社用3、とても高い。\n32K context, プロンプト: $0.06 / 1K tokens, 補完: $0.12 / 1K tokens",
                Placeholder = "あなたの質問",
                MasterPrompt = _chat.DefaultPrompt,
                UseAzureOpenAI = true,
                GptModel = options.Value.Model3,
                IsPublic = true,
                MaxTokens = 2048
            });

            // 開発モードの場合
            if (!_options.IsLocalDevelopMode)
            {
                // _pageDataからIsPublicではないものを除外する
                _pageData = _pageData.Where(x => x.Value.IsPublic).ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public List<Display> GetKeys()
        {
            if (!_options.IsLocalDevelopMode)
            {
                // IsPublic = falseのものは除外する
                return _pageData.Where(x => x.Value.IsPublic).Select(x => x.Value).ToList();
            }
            return _pageData.Select(x => x.Value).ToList();
        }
    }

    /// <summary>
    /// 画面データ
    /// </summary>
    public class Display
    {
        /// <summary>アドレス</summary>
        public string Address { get; set; } = "Azure3";
        /// <summary>NavMenuに表示するタイトル</summary>
        public string Title { get; set; } = "Azure:gpt-35-turbo";
        public string Headline { get; set; } = "Azure OpenAI Service:gpt-35-turbo";
        public string Introduction { get; set; } = "会社用1。\n8K context, $0.002 / 1K tokens";
        public string Placeholder { get; set; } = "あなたの質問";
        /// <summary>送信ボタン</summary>
        public string SubmitText { get; set; } = "送信";
        ///// <summary>
        ///// 0から2の範囲で指定するけど、大きすぎると壊れる
        ///// </summary>
        //public double Temperature { get; set; } = 2.0;
        /// <summary>
        /// マスター扱い。上書きとかしないこと（開発モードは別）
        /// </summary>
        public List<ChatMessage> MasterPrompt { get; set; } = new();

        /// <summary>これがfalseの場合、"IsLocalDevelopMode": falseならば表示しない</summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>ユーザに入力させる項目</summary>
        public List<DisplayOption> Options { get; set; } = new();

        /// <summary>
        /// 実際に画面で使用するプロンプト
        /// </summary>
        public List<ChatMessage> CurrentPrompt { get; set; } = new();

        /// <summary>
        /// Azureを使う
        /// </summary>
        public bool UseAzureOpenAI { get; set; } = false;

        /// <summary>
        /// GPTのモデル
        /// OpenAI:gpt-3.5-turbo, gpt-4, gpt-4-0314
        /// MS:gpt-35-turbo, code-davinci-002, gpt-4, gpt-4-32k（デプロイ名で指定する必要があるので、名前はAzure OpenAI Studioを見ること。）
        /// </summary>
        public string GptModel { get; set; } = "gpt-3.5-turbo";

        /// <summary>
        /// code-davinci等はこっち
        /// </summary>
        public bool IsCode { get; set; } = false;

        /// <summary>
        /// 返答のトークン上限を設定
        /// 主にトークン数を節約するため。
        /// </summary>
        public int MaxTokens { get; set; } = 280;

        /// <summary>
        /// Option入力後に呼ぶ
        /// CurrentPromptを新しく作り直す。
        /// Option.Valueの内容をPromptの0番目のContentに適用する。
        /// </summary>
        public void ApplyOption()
        {
            CurrentPrompt.Clear();
            CurrentPrompt.AddRange(MasterPrompt);

            if (CurrentPrompt.Count == 0)
            {
                return;
            }
            var values = Options.Select(x => x.Value ?? string.Empty).ToArray();
            var content = string.Format(CurrentPrompt[0].Content, values);
            CurrentPrompt[0] = new ChatMessage(MasterPrompt[0].Role, content);
        }
    }

    /// <summary>
    /// ユーザが入力する設定項目
    /// </summary>
    public class DisplayOption
    {
        public string Subject { get; set; } = "あなたの名前";
        public string Value { get; set; } = "";
        /// <summary>
        /// 入力行数
        /// 2以上だとマルチライン入力欄にする
        /// </summary>
        public int Rows { get; set; } = 1;
    }

    /// <summary>
    /// 設定項目
    /// </summary>
    public class ChatOption
    {
        /// <summary>
        /// trueだと開発モードとなり非公開のプロンプトも適用される
        /// </summary>
        public bool IsLocalDevelopMode { get; set; } = false;

        // 会社用
        public string Model1 { get; set; } = string.Empty;
        public string Model2 { get; set; } = string.Empty;
        public string Model3 { get; set; } = string.Empty;
        public string Model4 { get; set; } = string.Empty;
    }

}