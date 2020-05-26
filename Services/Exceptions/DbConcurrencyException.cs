using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class DbconcurrencyException: ApplicationException
    {
        public DbconcurrencyException(string message):base(message)
        {

        }
    }
}
