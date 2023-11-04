using System;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
	public class MyLogger: EntityBase<Guid>
	{
        public string logMessage { get; set; }

        public MyLogger(string logMessage)
        {
            this.CreatedOn = DateTime.UtcNow;
            this.logMessage = logMessage;
        }
    }
}

