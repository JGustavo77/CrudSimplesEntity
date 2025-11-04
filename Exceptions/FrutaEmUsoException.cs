using System;

namespace FrutasDoSeuZe.Exceptions
{
    public class FrutaEmUsoException : InvalidOperationException
    {
        public FrutaEmUsoException(string message) : base(message) { }
    }
}
