using System;
using System.IO;
using MarchzinsBonusTool.Infrastructure.Exceptions;

namespace MarchzinsBonusTool.Infrastructure.ErrorHandling
{
    public static class ErrorHandler
    {
        private static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MarchzinsBonusTool",
            "logs",
            "error.log");

        public static string HandleException(Exception ex, string context = null)
        {
            var errorMessage = FormatErrorMessage(ex, context);
            LogError(errorMessage);

            if (ex is MarchzinsException marchzinsEx)
            {
                return marchzinsEx.UserFriendlyMessage;
            }

            return GetGenericUserMessage(ex);
        }

        public static void LogError(string message)
        {
            try
            {
                var directory = Path.GetDirectoryName(LogPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(LogPath, logEntry);
            }
            catch
            {
                // Ignore logging errors
            }
        }

        public static void ValidateRange(decimal value, decimal min, decimal max, string fieldName)
        {
            if (value < min || value > max)
            {
                throw new ValidationException(fieldName, $"{fieldName} must be between {min} and {max}");
            }
        }

        private static string FormatErrorMessage(Exception ex, string context)
        {
            var message = $"ERROR: {ex.Message}";
            if (!string.IsNullOrEmpty(context))
                message = $"{context} - {message}";
            return message;
        }

        private static string GetGenericUserMessage(Exception ex)
        {
            return ex switch
            {
                UnauthorizedAccessException => "Zugriff verweigert.",
                FileNotFoundException => "Datei nicht gefunden.",
                _ => "Ein Fehler ist aufgetreten."
            };
        }
    }
}