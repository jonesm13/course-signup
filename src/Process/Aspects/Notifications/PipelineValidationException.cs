namespace Process.Aspects.Notifications
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.Results;

    public class PipelineValidationException : Exception
    {
        public IEnumerable<ValidationFailure> Failures { get; }

        public PipelineValidationException(
            IEnumerable<ValidationFailure> failures)
        {
            Failures = failures;
        }
    }
}