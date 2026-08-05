using STranslate.Plugin;
using STranslate.ViewModels;
using System.Windows.Controls;

namespace STranslate.Tests;

public class OcrLocationWarningTests
{
    [Theory]
    [InlineData(false, false, false)]
    [InlineData(true, false, true)]
    [InlineData(true, true, false)]
    public void WarningIsOnlyShownWhenSupportedLocationDataIsMissing(
        bool supportsBoxPoints,
        bool resultHasBoxPoints,
        bool expected)
    {
        var plugin = new TestOcrPlugin(supportsBoxPoints);
        var result = new OcrResult
        {
            OcrContents =
            [
                new()
                {
                    Text = "text",
                    BoxPoints = resultHasBoxPoints
                        ? [new(0, 0), new(10, 0), new(10, 10), new(0, 10)]
                        : []
                }
            ]
        };

        Assert.Equal(expected, OcrWindowViewModel.ShouldShowNoLocationInfo(plugin, result));
    }

    private sealed class TestOcrPlugin(bool supportsBoxPoints) : IOcrPlugin
    {
        public IEnumerable<LangEnum> SupportedLanguages => [LangEnum.Auto];

        public bool SupportBoxPoints() => supportsBoxPoints;

        public void Init(IPluginContext context)
        {
        }

        public Control GetSettingUI() => new();

        public Task<OcrResult> RecognizeAsync(
            OcrRequest request,
            CancellationToken cancellationToken = default) => Task.FromResult(new OcrResult());

        public void Dispose()
        {
        }
    }
}
