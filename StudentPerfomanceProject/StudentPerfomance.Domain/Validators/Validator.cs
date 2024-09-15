using System.Text;

namespace StudentPerfomance.Domain.Validators;

internal abstract class Validator<T> where T : class
{
    protected readonly StringBuilder _errorBuilder;
    public Validator() => _errorBuilder = new StringBuilder();
    public abstract bool Validate();
    public string GetErrorText() => _errorBuilder.ToString();
}
