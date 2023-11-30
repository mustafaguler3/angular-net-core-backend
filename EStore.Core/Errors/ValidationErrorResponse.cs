using System;
using EStore.Core.Exceptions;

namespace EStore.Core.Errors
{
	public class ValidationErrorResponse : ApiResponse
	{
		public IEnumerable<string> Errors { get; set; }


		public ValidationErrorResponse():base(400)
		{
		}
	}
}

