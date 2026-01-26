using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiXamlCsharpSample;

public class MainViewModel : INotifyPropertyChanged
{
    private string _name = "World";
    private double _price = 9.99;
    private double _quantity = 1;
    private bool _isHidden = false;
    private bool _hasAccount = true;
    private bool _agreedToTerms = false;
    private int _clickCount = 0;
    private string _statusMessage = "";

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(Greeting)); }
    }

    public double Price
    {
        get => _price;
        set { _price = value; OnPropertyChanged(); }
    }

    public double Quantity
    {
        get => _quantity;
        set { _quantity = value; OnPropertyChanged(); }
    }

    public bool IsHidden
    {
        get => _isHidden;
        set { _isHidden = value; OnPropertyChanged(); }
    }

    public bool HasAccount
    {
        get => _hasAccount;
        set { _hasAccount = value; OnPropertyChanged(); }
    }

    public bool AgreedToTerms
    {
        get => _agreedToTerms;
        set { _agreedToTerms = value; OnPropertyChanged(); }
    }

    public int ClickCount
    {
        get => _clickCount;
        set { _clickCount = value; OnPropertyChanged(); }
    }

    public void IncrementCount() => ClickCount++;

    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    // For traditional binding comparison
    public string Greeting => $"Hello, {Name}!";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
