using System.Text.RegularExpressions;
using ISI.Domain.Exceptions;

namespace ISI.Domain.Validation;

public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
            throw new EntityValidationException(
                $"{fieldName} should not be null");
    }
    
    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException(
                $"{fieldName} should not be empty or null");
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target.Length < minLength)
            throw new EntityValidationException($"{fieldName} should be at least {minLength} characters long");
    }
    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
            throw new EntityValidationException($"{fieldName} should be less or equal {maxLength} characters long");
    }
    
    public static void MatchRegex(string target, string regexPattern, string fieldName)
    {
        if (!Regex.IsMatch(target, regexPattern))
            throw new EntityValidationException($"{fieldName} does not match the required pattern: {regexPattern}");
    }
    
    public static void GreaterThanOrEqualDate(DateTime targetDate, DateTime comparisonDate, string fieldName)
    {
        if (targetDate < comparisonDate)
            throw new EntityValidationException($"{fieldName} should be greater than or equal to {comparisonDate.ToShortDateString()}");
    }

    public static void LessThanOrEqualDate(DateTime targetDate, DateTime comparisonDate, string fieldName)
    {
        if (targetDate > comparisonDate)
            throw new EntityValidationException($"{fieldName} should be less than or equal to {comparisonDate.ToShortDateString()}");
    }
    
}