namespace KVA.Cinema.Exceptions
{
    using System;

    internal class DuplicatedEntityException : Exception
    {
        public DuplicatedEntityException(string message) : base(message)
        {

        }
    }
}
