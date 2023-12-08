namespace KVA.Cinema.Exceptions
{
    using System;

    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}
