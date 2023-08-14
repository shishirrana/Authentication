using System.ComponentModel.DataAnnotations;

namespace reviseAuthentication.Filter
{
    public class CustomValidationAttribute : ValidationAttribute
    {
        //assigning private property for minlength and maxlength

        private int _maxlength;
        private int _minlength; 

        public CustomValidationAttribute(int maxlength, int minlength)
        {
            _minlength = minlength;
            _maxlength = maxlength;
        }

        public override bool IsValid(object? value)
        {
            //validation
            //minimum length 5
            string val = (value as string)??"";

            if (val.Length < _minlength) 
            {
                ErrorMessage = "The lenght of the word should be greater than 5";
                return false;

            }

            //max length 50

            if (val.Length > _maxlength) 
            {
                ErrorMessage = "The length of the word should be less than 50";
                return false;
            }

            return true;
        }
    }
}
