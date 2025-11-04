using System;

namespace FrutasDoSeuZe.Exceptions
{
    public class FrutaNaoEncontradaException : ApplicationException
    {
        public FrutaNaoEncontradaException(string message) : base(message) { }
    }
}
