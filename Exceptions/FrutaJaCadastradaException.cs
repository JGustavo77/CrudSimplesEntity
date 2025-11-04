using System;

namespace FrutasDoSeuZe.Exceptions
{
    public class FrutaJaCadastradaException : ApplicationException
    {
        public FrutaJaCadastradaException(string message) : base(message) { }
    }
}
