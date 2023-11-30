using System;
namespace EStore.Core.Exceptions
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public ApiResponse(int statusCode,string message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetMessageForStatusCode(statusCode);
		}

		private string GetMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "A Bad request, you have made",
				401 => "Authorized , you are not",
				404 => "Resource found, it was not",
				500 => "Errors are the path to the dark side"
			};
		}
	}
}

