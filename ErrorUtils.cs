using System.Collections.Generic;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using utils_lib.Dto.Error;

namespace utils_lib
{
    public static class ErrorUtils
    {
        public static IList<FieldErrorDto> GetFieldErrors(ModelStateDictionary modelState)
        {
            IList<FieldErrorDto> fieldErrors = new List<FieldErrorDto>();

            var htmlSanitizer = new HtmlSanitizer();

            foreach (var (key, value) in modelState)
                if (value.ValidationState == ModelValidationState.Invalid)
                    fieldErrors.Add(new FieldErrorDto
                    {
                        Name = char.ToLower(key[0]) + key.Substring(1),
                        Status = htmlSanitizer.Sanitize(value.Errors[0].ErrorMessage)
                    });

            return fieldErrors;
        }
    }
}