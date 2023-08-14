using reviseAuthentication.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace reviseAuthentication.Filter
{
    public class MyEmailValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return Regex.IsMatch((value as string)
                ?? "", @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        }
    }
}
