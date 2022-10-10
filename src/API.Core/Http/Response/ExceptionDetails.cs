using System;
using System.Collections;
using System.Collections.Generic;

namespace API.Core.Http.Response
{
    public class ExceptionDetails
    {
        public static Dictionary<string, string> FromException(Exception exception, bool showDetails)
        {
            var details = new Dictionary<string, string>();

            if (showDetails) {
                details.Add("type", exception.GetType().ToString());
                details.Add("message", exception.Message);
                details.Add("innerMessage", exception.InnerException?.Message);
                details.Add("stackTrace", exception.StackTrace);

                foreach (DictionaryEntry data in exception.Data) {
                    if (data.Value != null) {
                        details.Add(data.Key.ToString() ?? string.Empty, data.Value.ToString());
                    }
                }
            }

            return details;
        }
    }
}
