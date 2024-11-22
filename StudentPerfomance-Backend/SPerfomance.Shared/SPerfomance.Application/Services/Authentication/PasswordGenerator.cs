using SPerfomance.Application.Services.Authentication.Abstractions;

namespace SPerfomance.Application.Services.Authentication;

public class PasswordGenerator : IPasswordGenerator
{
    private const string _characters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";

    public string Generate()
    {
        const int length = 10;
        var random = new Random();
        var password = new string(
            Enumerable.Repeat(_characters, length).Select(s => s[random.Next(s.Length)]).ToArray()
        );
        return password;
    }
}
