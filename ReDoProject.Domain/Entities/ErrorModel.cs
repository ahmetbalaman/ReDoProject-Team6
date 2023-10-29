using System;
namespace ReDoProject.Domain.Entities
{
	public class ErrorModel
	{
		public int ErrorResponseType { get; set; }
		public List<String>? ErrorMessage { get; set; }
	}
}

