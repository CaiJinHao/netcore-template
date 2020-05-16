using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Utility.CoustomValidationAttribute
{

    [AttributeUsage(AttributeTargets.Property)]
    public class RegexValidationAttribute : ValidationAttribute
    {
        public RegexValidationAttribute(string _regex)
        {
            RegexStr = _regex;
        }

        public string RegexStr { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var regex = new Regex(RegexStr);
            if (!regex.IsMatch((string)value))
            {
                return new ValidationResult($"{validationContext.MemberName}格式错误");
            }
            return ValidationResult.Success;
        }
    }
}
