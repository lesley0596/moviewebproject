// Custom Validator - can be used via [UrlResource] on model attribute
using System.ComponentModel.DataAnnotations;

namespace MMS.Data.Validators;

public class UrlResource : ValidationAttribute {  
    protected override ValidationResult IsValid(object value, ValidationContext ctx)
    {
        string url = (string)value;   // extract url from validation value

        // check a url was provided and is points to a valid resource
        if (url != null &&  !UrlResourceExists(url))
        {
            return new ValidationResult($"The {ctx.DisplayName} field URL resource does not exist");
        } 
        return ValidationResult.Success; 
    }

    // verify url points to a valid resource
    private bool UrlResourceExists(string url)  {
        // create a httpclient to make request
        using(var http = new HttpClient())
        {  
            var valid = false;
            try {
                var result = http.SendAsync( new HttpRequestMessage(HttpMethod.Head, url) ).Result;
                valid = result.IsSuccessStatusCode;
            } catch (Exception) {}    
            return valid; 
        }
    }
}
