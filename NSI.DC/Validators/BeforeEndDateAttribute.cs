using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace NSI.DC.Validators
{
    public class BeforeEndDateAttribute : ValidationAttribute
    {
        public string EndDatePropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(EndDatePropertyName);

            DateTime? endDate = (DateTime?)endDateProperty.GetValue(validationContext.ObjectInstance, null);

            var startDate = DateTime.Parse(value.ToString());

            if (endDate == null || endDate >= startDate) return ValidationResult.Success;
            return new ValidationResult("End date must be greater than start date");
        }
    }
}
