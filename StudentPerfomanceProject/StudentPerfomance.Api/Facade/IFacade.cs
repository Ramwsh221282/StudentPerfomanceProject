using Microsoft.AspNetCore.Mvc;

namespace StudentPerfomance.Api.Facade;

internal interface IFacade<T>
{
	public Task<ActionResult<T>> Process();
}
