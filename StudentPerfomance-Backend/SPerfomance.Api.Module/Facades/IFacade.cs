using Microsoft.AspNetCore.Mvc;

namespace SPerfomance.Api.Module.Facades;

public interface IFacade<T>
{
	public Task<ActionResult<T>> Process();
}
