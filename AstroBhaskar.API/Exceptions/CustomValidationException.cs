using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBhaskar.API.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(ModelStateDictionary modelState, string message = "Validation Failed!") : base(message)
        {
            this.Errors = modelState.Where<KeyValuePair<string, ModelStateEntry>>(
                (Func<KeyValuePair<string, ModelStateEntry>, bool>)(
                    x =>
                    {
                        var modelStateEntry = x.Value;
                        return modelStateEntry != null && modelStateEntry.Errors.Count > 0;
                    })).ToDictionary<KeyValuePair<string, ModelStateEntry>, string, List<string>>(
                (Func<KeyValuePair<string, ModelStateEntry>, string>)(kvp => kvp.Key),
                (Func<KeyValuePair<string, ModelStateEntry>, List<string>>)(
                    kvp =>
                    {
                        var modelStateEntry = kvp.Value;
                        return (modelStateEntry != null
                            ? modelStateEntry.Errors
                                .Select<ModelError, string>((Func<ModelError, string>)(x => x.ErrorMessage))
                                .ToList<string>()
                            : (List<string>)null) ?? new List<string>();
                    }));
        }

        public string ErrorResponseType { get; set; } = "InvalidInput";
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
