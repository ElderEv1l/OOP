using System.Runtime.CompilerServices;

namespace Banks.Entities;

public class ClientBuilder
{
    private string _name = string.Empty;
    private string _address = string.Empty;
    private string _passport = string.Empty;

    public ClientBuilder()
    {
        Reset();
    }

    public ClientBuilder SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(nameof(name))) throw new ArgumentNullException(nameof(name));
        _name = name;
        return this;
    }

    public ClientBuilder SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(nameof(address))) throw new ArgumentNullException(nameof(address));
        _address = address;
        return this;
    }

    public ClientBuilder SetPassport(string passport)
    {
        if (string.IsNullOrWhiteSpace(nameof(passport))) throw new ArgumentNullException(nameof(passport));
        _passport = passport;
        return this;
    }

    public Client GetClient()
    {
        var res = new Client(_name, _address, _passport);
        Reset();
        return res;
    }

    private void Reset()
    {
        _name = string.Empty;
        _address = string.Empty;
        _passport = string.Empty;
    }
}