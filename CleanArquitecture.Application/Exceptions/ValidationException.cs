using FluentValidation.Results;

namespace CleanArquitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public ValidationException() : base("Se presentaron errores de validación.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> validationFailures) 
        {
            Errors = validationFailures
                        .GroupBy(error => error.PropertyName, error => error.ErrorMessage)
                        .ToDictionary(group => group.Key, group => group.ToArray());
        }

    }
}
