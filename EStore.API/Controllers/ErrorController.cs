using System;
using EStore.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EStore.API.Controllers
{
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : BaseController
	{
		public IActionResult Error(int code)
		{
			return new ObjectResult(new ApiResponse(code));
		}
	}
}

