namespace KVA.Cinema.Exceptions
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class FailedToCreateEntityException : Exception
    {
        public IEnumerable Errors { get; set; }

        public FailedToCreateEntityException(IEnumerable errors)
        {
            Errors = errors;
        }
    }
}
