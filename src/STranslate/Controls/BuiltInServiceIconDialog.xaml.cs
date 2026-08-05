using iNKORE.UI.WPF.Modern.Controls;
using STranslate.Core;
using STranslate.Plugin;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;

namespace STranslate.Controls;

/// <summary>
/// Provides searchable selection of the service icons embedded in the application.
/// </summary>
public partial class BuiltInServiceIconDialog : ContentDialog, INotifyPropertyChanged
{
    private readonly CollectionViewSource _collectionViewSource = new()
    {
        Source = BuiltInServiceIconCatalog.Icons
    };

    private string _filterText = string.Empty;
    private BuiltInServiceIcon? _selectedIcon;

    public BuiltInServiceIconDialog(Service service)
    {
        _collectionViewSource.Filter += OnFilter;
        _selectedIcon = BuiltInServiceIconCatalog.GetSelectedIcon(service);

        InitializeComponent();
        DataContext = this;
    }

    public ICollectionView Icons => _collectionViewSource.View;

    public string FilterText
    {
        get => _filterText;
        set
        {
            if (_filterText == value)
                return;

            _filterText = value;
            Icons.Refresh();
            if (SelectedIcon != null && !MatchesFilter(SelectedIcon))
                SelectedIcon = null;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasMatches));
        }
    }

    public BuiltInServiceIcon? SelectedIcon
    {
        get => _selectedIcon;
        set
        {
            if (_selectedIcon == value)
                return;

            _selectedIcon = value;
            OnPropertyChanged();
        }
    }

    public bool HasMatches => !Icons.IsEmpty;

    private void OnFilter(object sender, FilterEventArgs e)
        => e.Accepted = e.Item is BuiltInServiceIcon icon && MatchesFilter(icon);

    private bool MatchesFilter(BuiltInServiceIcon icon)
        => string.IsNullOrWhiteSpace(FilterText) ||
           icon.SearchText.Contains(FilterText.Trim(), StringComparison.OrdinalIgnoreCase);

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key is not Key.F || Keyboard.Modifiers is not ModifierKeys.Control)
            return;

        PART_FilterTextBox.Focus();
        PART_FilterTextBox.SelectAll();
        e.Handled = true;
    }

    private void OnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
    {
        _collectionViewSource.Filter -= OnFilter;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public event PropertyChangedEventHandler? PropertyChanged;
}
