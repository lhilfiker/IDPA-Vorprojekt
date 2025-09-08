namespace MarchzinsBonusTool.Business
{
    /// <summary>
    /// Represents the result of a validation operation.
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        public ValidationResult(bool isValid, string errorMessage)
        {
        }

        public static ValidationResult Success()
        {
        }

        public static ValidationResult Failure(string errorMessage)
        {
        }
    }
}