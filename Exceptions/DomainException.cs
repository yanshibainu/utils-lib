using System;

namespace utils_lib.Exceptions
{
    public class DomainException: Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}