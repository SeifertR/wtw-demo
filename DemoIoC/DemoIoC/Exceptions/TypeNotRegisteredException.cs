using System;

namespace DemoIoC.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string msg) : base(msg)
        {
        }
    }
}
