using STranslate.Plugin;
using System.Collections.ObjectModel;
using System.IO;

namespace STranslate.Core;

/// <summary>
/// Describes an icon embedded in the application for use by a service instance.
/// </summary>
public sealed record BuiltInServiceIcon(
    string Key,
    string DisplayName,
    string ResourceFileName,
    string Aliases = "")
{
    public Uri ResourceUri { get; } = new(
        $"/STranslate;component/Resources/BuiltInServiceIcons/{ResourceFileName}",
        UriKind.Relative);

    public string SearchText { get; } = $"{Key} {DisplayName} {Aliases}";
}

internal static class BuiltInServiceIconCatalog
{
    internal const string FileMarker = ".builtin.";

    public static ReadOnlyCollection<BuiltInServiceIcon> Icons { get; } = Array.AsReadOnly<BuiltInServiceIcon>(
    [
        new("stranslate", "STranslate", "stranslate.ico", "local 本地"),
        new("deepl", "DeepL", "deepl.png"),
        new("baidu", "Baidu", "baidu.png", "百度"),
        new("google", "Google", "google.png", "谷歌"),
        new("iciba", "iCiba", "iciba.png", "爱词霸 金山"),
        new("youdao", "Youdao", "youdao.png", "有道"),
        new("bing", "Bing", "bing.png", "必应"),
        new("openai", "OpenAI", "openai.png", "ChatGPT"),
        new("gemini", "Gemini", "gemini.png"),
        new("tencent", "Tencent", "tencent.png", "腾讯"),
        new("ali", "Alibaba", "ali.png", "阿里"),
        new("niutrans", "NiuTrans", "niutrans.png", "小牛"),
        new("caiyun", "Caiyun", "caiyun.png", "彩云"),
        new("microsoft", "Microsoft", "microsoft.png", "微软"),
        new("volcengine", "Volcengine", "volcengine.png", "火山引擎"),
        new("ecdict", "ECDICT", "ecdict.png", "简明英汉词典"),
        new("azure", "Azure", "azure.png"),
        new("chatglm", "ChatGLM", "chatglm.png", "智谱清言"),
        new("linyi", "01.AI", "linyi.png", "零一万物"),
        new("deepseek", "DeepSeek", "deepseek.png"),
        new("groq", "Groq", "groq.png"),
        new("paddleocr", "PaddleOCR", "paddleocr.png", "飞桨"),
        new("baidubce", "Baidu BCE", "baidubce.png", "百度云"),
        new("tencentocr", "Tencent OCR", "tencent2.png", "腾讯文字识别"),
        new("ollama", "Ollama", "ollama.png"),
        new("kimi", "Kimi", "kimi.png", "月之暗面"),
        new("lingva", "Lingva", "lingva.png"),
        new("wechat", "WeChat", "wechat.png", "微信"),
        new("claude", "Claude", "claude.png", "Anthropic"),
        new("eudict", "Eudic", "eudict.png", "欧路 欧陆词典"),
        new("yandex", "Yandex", "yandex.png"),
        new("deerapi", "DeerAPI", "deerapi.png"),
        new("grok", "Grok", "grok.png", "xAI"),
        new("bailian", "Model Studio", "bailian.png", "阿里百炼 Bailian"),
        new("transmart", "Transmart", "transmart.png"),
        new("openrouter", "OpenRouter", "openrouter.png"),
        new("maimemo", "Maimemo", "maimemo.png", "墨墨背单词"),
        new("mtranserver", "MTranServer", "mtranserver.png")
    ]);

    public static string BuildServiceFileName(string serviceId, BuiltInServiceIcon icon)
        => $"{serviceId}{FileMarker}{icon.Key}{Path.GetExtension(icon.ResourceFileName)}";

    public static BuiltInServiceIcon? GetSelectedIcon(Service service)
    {
        var fileName = Path.GetFileName(service.IconPath);
        var prefix = $"{service.ServiceID}{FileMarker}";
        if (!fileName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            return null;

        var key = Path.GetFileNameWithoutExtension(fileName[prefix.Length..]);
        return Icons.FirstOrDefault(icon => icon.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
    }
}
