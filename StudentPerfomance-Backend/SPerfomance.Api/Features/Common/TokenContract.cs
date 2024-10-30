namespace SPerfomance.Api.Features.Common;

public class TokenContract
{
	public string Token { get; set; } = null!;

	public static implicit operator Token(TokenContract contract) =>
		new Token(contract.Token);
}
