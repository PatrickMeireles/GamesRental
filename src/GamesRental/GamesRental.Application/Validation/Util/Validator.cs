using System;
using System.Collections.Generic;
using System.Linq;

namespace GamesRental.Application.Validation.Util
{
    public class FieldValidator
    {
        public string field { get; set; }

        public string error { get; set; }

        public FieldValidator(string _field, string _error)
        {
            this.field = _field;
            this.error = _error;
        }
    }
    public class Validator
    {
        public IEnumerable<FieldValidator> fieldErrors { get; set; }

        public IEnumerable<String> errors { get; set; }

        public Validator()
        {
            this.fieldErrors = new List<FieldValidator>();
            this.errors = new List<string>();
        }

        public Validator(FluentValidation.Results.ValidationResult results)
        {
            if (!results.IsValid)
            {
                var erros = results.Errors;

                fieldErrors = erros.Where(x => !String.IsNullOrEmpty(x.PropertyName))
                                   .Select(x => new FieldValidator(x.PropertyName, x.ErrorMessage));

                errors = erros.Where(x => !fieldErrors.Any() && String.IsNullOrEmpty(x.PropertyName))
                              .Select(x => x.ErrorMessage);
            }
        }
    }
}
