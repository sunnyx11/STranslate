using CommunityToolkit.Mvvm.Input;
using STranslate.ViewModels;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace STranslate.Tests;

public class TranslationCommandConcurrencyTests
{
    [Fact]
    public async Task TranslateCommandRemainsExecutableWhilePreviousRequestIsRunning()
    {
        var viewModel = (MainWindowViewModel)RuntimeHelpers.GetUninitializedObject(
            typeof(MainWindowViewModel));
        var generatedCommand = Assert.IsType<AsyncRelayCommand<object?>>(viewModel.TranslateCommand);
        var optionsField = typeof(AsyncRelayCommand<object?>).GetField(
            "options",
            BindingFlags.Instance | BindingFlags.NonPublic);
        var options = Assert.IsType<AsyncRelayCommandOptions>(optionsField?.GetValue(generatedCommand));

        var releaseExecution = new TaskCompletionSource(
            TaskCreationOptions.RunContinuationsAsynchronously);
        var command = new AsyncRelayCommand<object?>(
            (_, _) => releaseExecution.Task,
            _ => true,
            options);

        var execution = command.ExecuteAsync(null);

        Assert.True(command.IsRunning);
        Assert.True(command.CanExecute(null));

        releaseExecution.SetResult();
        await execution;
    }
}
