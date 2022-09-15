using System;

namespace board
{
    internal class ExceptionBoard : Exception
    {
        public ExceptionBoard(string msg) : base(msg)
        {
        }
    }
}
