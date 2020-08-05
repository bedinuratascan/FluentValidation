using FluentValidation;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} cannot be empty.";
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress().WithMessage("Email field must be correct formatted.");
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(1, 70).WithMessage("Age range must be from 18 to 60.");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                return DateTime.Now.AddYears(-18)>=x;
            }).WithMessage("Your age must be greater than 18");

            RuleFor(x => x.Gender).IsInEnum().WithMessage("{PropertyName} field must be 1 for male and 2 for female");

            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}
