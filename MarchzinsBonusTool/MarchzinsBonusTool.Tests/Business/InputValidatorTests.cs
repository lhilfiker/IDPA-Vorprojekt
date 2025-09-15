using System;
using Xunit;
using MarchzinsBonusTool.Business;
using MarchzinsBonusTool.ViewModels;

namespace MarchzinsBonusTool.Tests.Business
{
    public class InputValidatorTests
    {
        private readonly InputValidator _validator;

        public InputValidatorTests()
        {
            _validator = new InputValidator();
        }

        #region ValidateSparkapital Tests

        [Fact]
        public void ValidateSparkapital_WithValidAmount_ReturnsSuccess()
        {
            var result = _validator.ValidateSparkapital("1000.50");
            
            Assert.True(result.IsValid);
            Assert.Empty(result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithNullInput_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital(null);
            
            Assert.False(result.IsValid);
            Assert.Equal("Sparkapital ist erforderlich", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithEmptyInput_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital("");
            
            Assert.False(result.IsValid);
            Assert.Equal("Sparkapital ist erforderlich", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithWhitespaceInput_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital("   ");
            
            Assert.False(result.IsValid);
            Assert.Equal("Sparkapital ist erforderlich", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithInvalidFormat_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital("abc");
            
            Assert.False(result.IsValid);
            Assert.Equal("Ungültiges Zahlenformat", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithZeroValue_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital("0");
            
            Assert.False(result.IsValid);
            Assert.Equal("Sparkapital muss grösser als 0 sein", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithNegativeValue_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital("-100");
            
            Assert.False(result.IsValid);
            Assert.Equal("Sparkapital muss grösser als 0 sein", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSparkapital_WithTooLargeValue_ReturnsFailure()
        {
            var result = _validator.ValidateSparkapital("20000000000");
            
            Assert.False(result.IsValid);
            Assert.Equal("Sparkapital ist zu gross", result.ErrorMessage);
        }

        [Theory]
        [InlineData("1'000.50")]
        [InlineData("1 000.50")]
        [InlineData("1000,50")]
        public void ValidateSparkapital_WithDifferentFormats_ReturnsSuccess(string input)
        {
            var result = _validator.ValidateSparkapital(input);
            
            Assert.True(result.IsValid);
        }

        #endregion

        #region ValidateZinssatz Tests

        [Fact]
        public void ValidateZinssatz_WithValidRate_ReturnsSuccess()
        {
            var result = _validator.ValidateZinssatz("2.5", "Test Rate");
            
            Assert.True(result.IsValid);
            Assert.Empty(result.ErrorMessage);
        }

        [Fact]
        public void ValidateZinssatz_WithNullInput_ReturnsFailure()
        {
            var result = _validator.ValidateZinssatz(null, "Test Rate");
            
            Assert.False(result.IsValid);
            Assert.Equal("Test Rate ist erforderlich", result.ErrorMessage);
        }

        [Fact]
        public void ValidateZinssatz_WithEmptyInput_ReturnsFailure()
        {
            var result = _validator.ValidateZinssatz("", "Test Rate");
            
            Assert.False(result.IsValid);
            Assert.Equal("Test Rate ist erforderlich", result.ErrorMessage);
        }

        [Fact]
        public void ValidateZinssatz_WithInvalidFormat_ReturnsFailure()
        {
            var result = _validator.ValidateZinssatz("invalid", "Test Rate");
            
            Assert.False(result.IsValid);
            Assert.Equal("Ungültiges Zahlenformat", result.ErrorMessage);
        }

        [Fact]
        public void ValidateZinssatz_WithNegativeValue_ReturnsFailure()
        {
            var result = _validator.ValidateZinssatz("-1", "Test Rate");
            
            Assert.False(result.IsValid);
            Assert.Equal("Test Rate darf nicht negativ sein", result.ErrorMessage);
        }

        [Fact]
        public void ValidateZinssatz_WithValueAbove100_ReturnsFailure()
        {
            var result = _validator.ValidateZinssatz("101", "Test Rate");
            
            Assert.False(result.IsValid);
            Assert.Equal("Test Rate darf nicht grösser als 100% sein", result.ErrorMessage);
        }

        [Fact]
        public void ValidateZinssatz_WithZeroValue_ReturnsSuccess()
        {
            var result = _validator.ValidateZinssatz("0", "Test Rate");
            
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateZinssatz_WithMaxValue_ReturnsSuccess()
        {
            var result = _validator.ValidateZinssatz("100", "Test Rate");
            
            Assert.True(result.IsValid);
        }

        #endregion

        #region ValidateSteuersatz Tests

        [Fact]
        public void ValidateSteuersatz_WithValidRate_ReturnsSuccess()
        {
            var result = _validator.ValidateSteuersatz("25.5");
            
            Assert.True(result.IsValid);
            Assert.Empty(result.ErrorMessage);
        }

        [Fact]
        public void ValidateSteuersatz_WithNullInput_ReturnsFailure()
        {
            var result = _validator.ValidateSteuersatz(null);
            
            Assert.False(result.IsValid);
            Assert.Equal("Steuersatz ist erforderlich", result.ErrorMessage);
        }

        [Fact]
        public void ValidateSteuersatz_WithNegativeValue_ReturnsFailure()
        {
            var result = _validator.ValidateSteuersatz("-5");
            
            Assert.False(result.IsValid);
            Assert.Equal("Steuersatz darf nicht negativ sein", result.ErrorMessage);
        }

        #endregion

        #region ValidateDate Tests

        [Fact]
        public void ValidateDate_WithValidDate_ReturnsSuccess()
        {
            var date = new DateTime(1990, 5, 15);
            var result = _validator.ValidateDate(date);
            
            Assert.True(result.IsValid);
            Assert.Empty(result.ErrorMessage);
        }

        [Fact]
        public void ValidateDate_WithDateTooFarInPast_ReturnsFailure()
        {
            var date = new DateTime(1850, 1, 1);
            var result = _validator.ValidateDate(date);
            
            Assert.False(result.IsValid);
            Assert.Equal("Datum ist zu weit in der Vergangenheit", result.ErrorMessage);
        }

        [Fact]
        public void ValidateDate_WithDateTooFarInFuture_ReturnsFailure()
        {
            var date = DateTime.Now.AddYears(15);
            var result = _validator.ValidateDate(date);
            
            Assert.False(result.IsValid);
            Assert.Equal("Datum ist zu weit in der Zukunft", result.ErrorMessage);
        }

        [Fact]
        public void ValidateDate_WithBoundaryDate1900_ReturnsSuccess()
        {
            var date = new DateTime(1900, 1, 1);
            var result = _validator.ValidateDate(date);
            
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateDate_WithFutureBoundaryDate_ReturnsSuccess()
        {
            var date = DateTime.Now.AddYears(10);
            var result = _validator.ValidateDate(date);
            
            Assert.True(result.IsValid);
        }

        #endregion

        #region IsValidCalculation Tests

        [Fact]
        public void IsValidCalculation_WithNullModel_ReturnsFalse()
        {
            var result = _validator.IsValidCalculation(null);
            
            Assert.False(result);
        }

        [Fact]
        public void IsValidCalculation_WithValidModel_ReturnsTrue()
        {
            var model = new MainViewModel
            {
                SparkapitalInput = "1000",
                NormalerZinssatzInput = "2.5",
                BonusZinssatzInput = "1.0",
                SteuersatzInput = "25",
                GeburtsdatumInput = new DateTime(1990, 1, 1)
            };
            
            var result = _validator.IsValidCalculation(model);
            
            Assert.True(result);
        }

        [Fact]
        public void IsValidCalculation_WithInvalidSparkapital_ReturnsFalse()
        {
            var model = new MainViewModel
            {
                SparkapitalInput = "invalid",
                NormalerZinssatzInput = "2.5",
                BonusZinssatzInput = "1.0",
                SteuersatzInput = "25",
                GeburtsdatumInput = new DateTime(1990, 1, 1)
            };
            
            var result = _validator.IsValidCalculation(model);
            
            Assert.False(result);
        }

        [Fact]
        public void IsValidCalculation_WithInvalidDate_ReturnsFalse()
        {
            var model = new MainViewModel
            {
                SparkapitalInput = "1000",
                NormalerZinssatzInput = "2.5",
                BonusZinssatzInput = "1.0",
                SteuersatzInput = "25",
                GeburtsdatumInput = new DateTime(1850, 1, 1)
            };
            
            var result = _validator.IsValidCalculation(model);
            
            Assert.False(result);
        }

        #endregion
    }
}