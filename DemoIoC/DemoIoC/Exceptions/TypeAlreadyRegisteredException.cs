using System;

namespace DemoIoC.Exceptions
{
    public class TypeAlreadyRegisteredException : Exception
    {
        public TypeAlreadyRegisteredException(string msg) : base(msg)
        {
        }
    }
}
