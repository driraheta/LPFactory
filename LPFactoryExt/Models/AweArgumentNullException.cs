using System;

namespace LPFactory.Models
{
    public class AweArgumentNullException : ArgumentNullException
    {
        public AweArgumentNullException(string paramName) : base(paramName)
        {
        }
    }
}