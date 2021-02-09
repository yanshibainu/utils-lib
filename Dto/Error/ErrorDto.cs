using System.Collections.Generic;

namespace utils_lib.Dto.Error
{
    public class ErrorDto
    {
        public string Error { get; set; }

        public IList<FieldErrorDto> FieldErrors { get; set; }
    }
}