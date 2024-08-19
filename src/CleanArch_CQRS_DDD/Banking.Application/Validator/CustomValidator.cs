using Banking.Domain.Entities.Customers;
using Banking.Domain.Errors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banking.Application.Validator
{
    internal static class CustomValidator
    {
        public static IRuleBuilderOptionsConditions<T, string> IsValidSSN<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((ssn, context) =>
            {
                const string ssnPattern = @"^\d{3}-\d{2}-\d{4}$";

                if (!Regex.IsMatch(ssn, ssnPattern))
                    context.AddFailure("Value does not match the ssn pattern.");
            });
        }
    }
}