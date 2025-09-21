using System;
using Xunit;
using MarchzinsBonusTool.Commands;

namespace MarchzinsBonusTool.Tests.Commands
{
    public class RelayCommandTests
    {
        [Fact]
        public void Constructor_WithNullExecute_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new RelayCommand(null));
        }

        [Fact]
        public void Execute_WithValidAction_ExecutesAction()
        {
            bool actionExecuted = false;
            var command = new RelayCommand(_ => actionExecuted = true);
            
            command.Execute(null);
            
            Assert.True(actionExecuted);
        }

        [Fact]
        public void Execute_WithParameter_PassesParameterToAction()
        {
            object receivedParameter = null;
            var command = new RelayCommand(param => receivedParameter = param);
            var testParameter = "test";
            
            command.Execute(testParameter);
            
            Assert.Equal(testParameter, receivedParameter);
        }

        [Fact]
        public void CanExecute_WithNullPredicate_ReturnsTrue()
        {
            var command = new RelayCommand(_ => { });
            
            bool canExecute = command.CanExecute(null);
            
            Assert.True(canExecute);
        }

        [Fact]
        public void CanExecute_WithTruePredicate_ReturnsTrue()
        {
            var command = new RelayCommand(_ => { }, _ => true);
            
            bool canExecute = command.CanExecute(null);
            
            Assert.True(canExecute);
        }

        [Fact]
        public void CanExecute_WithFalsePredicate_ReturnsFalse()
        {
            var command = new RelayCommand(_ => { }, _ => false);
            
            bool canExecute = command.CanExecute(null);
            
            Assert.False(canExecute);
        }

        [Fact]
        public void CanExecute_WithParameterBasedPredicate_EvaluatesCorrectly()
        {
            var command = new RelayCommand(_ => { }, param => param != null);
            
            bool canExecuteWithNull = command.CanExecute(null);
            bool canExecuteWithValue = command.CanExecute("test");
            
            Assert.False(canExecuteWithNull);
            Assert.True(canExecuteWithValue);
        }

        [Fact]
        public void RaiseCanExecuteChanged_FiresEvent()
        {
            var command = new RelayCommand(_ => { });
            bool eventFired = false;
            
            command.CanExecuteChanged += (sender, args) => eventFired = true;
            command.RaiseCanExecuteChanged();
            
            Assert.True(eventFired);
        }

        [Fact]
        public void RaiseCanExecuteChanged_WithMultipleSubscribers_FiresForAll()
        {
            var command = new RelayCommand(_ => { });
            int eventCount = 0;
            
            command.CanExecuteChanged += (sender, args) => eventCount++;
            command.CanExecuteChanged += (sender, args) => eventCount++;
            command.RaiseCanExecuteChanged();
            
            Assert.Equal(2, eventCount);
        }

        [Fact]
        public void CanExecuteChanged_WithNoSubscribers_DoesNotThrow()
        {
            var command = new RelayCommand(_ => { });
            
            // Should not throw
            command.RaiseCanExecuteChanged();
        }
    }
}