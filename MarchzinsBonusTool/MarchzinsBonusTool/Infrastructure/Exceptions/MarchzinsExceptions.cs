using System;

namespace MarchzinsBonusTool.Infrastructure.Exceptions
{
    public class MarchzinsException : Exception
    {
        public string UserFriendlyMessage { get; }
        public string ErrorCode { get; }

        public MarchzinsException(string message, string userFriendlyMessage = null, string errorCode = null)
            : base(message)
        {
            UserFriendlyMessage = userFriendlyMessage ?? message;
            ErrorCode = errorCode ?? "GENERAL_ERROR";
        }

        public MarchzinsException(string message, Exception innerException, string userFriendlyMessage = null, string errorCode = null)
            : base(message, innerException)
        {
            UserFriendlyMessage = userFriendlyMessage ?? message;
            ErrorCode = errorCode ?? "GENERAL_ERROR";
        }
    }

    public class SettingsException : MarchzinsException
    {
        public SettingsException(string message, string userFriendlyMessage = null)
            : base(message, userFriendlyMessage, "SETTINGS_ERROR") { }

        public SettingsException(string message, Exception innerException, string userFriendlyMessage = null)
            : base(message, innerException, userFriendlyMessage, "SETTINGS_ERROR") { }
    }

    public class CalculationException : MarchzinsException
    {
        public CalculationException(string message, string userFriendlyMessage = null)
            : base(message, userFriendlyMessage, "CALCULATION_ERROR") { }

        public CalculationException(string message, Exception innerException, string userFriendlyMessage = null)
            : base(message, innerException, userFriendlyMessage, "CALCULATION_ERROR") { }
    }

    public class ValidationException : MarchzinsException
    {
        public string FieldName { get; }

        public ValidationException(string fieldName, string message, string userFriendlyMessage = null)
            : base(message, userFriendlyMessage, "VALIDATION_ERROR")
        {
            FieldName = fieldName;
        }
    }
}