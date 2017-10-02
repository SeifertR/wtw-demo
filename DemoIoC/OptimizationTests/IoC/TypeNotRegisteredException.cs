using System;

namespace OptimizationTests
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string msg) : base(msg)
        {
        }
    }
}
