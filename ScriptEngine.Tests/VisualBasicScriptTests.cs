using System;
using Xunit;

namespace ScriptEngine.Tests
{
    public class VisualBasicScriptTests
    {
        [Fact]
        public void Evaluate_NullInput_ExceptionThrown()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => VisualBasicScript.Evaluate<string>(null));
            Assert.Equal("code", ex.ParamName);
        }

        [Fact]
        public void Evaluate_SimpleArithmeticOperation_ResultReturned()
        {
            const string code = "Return 1 + 1";
            var result = VisualBasicScript.Evaluate<int>(code);

            Assert.Equal(2, result);
        }

        [Fact]
        public void Evaluate_MultipleSimpleArithmeticOperation_ResultsReturned()
        {
            for (var i = 0; i < 100; i++)
            {
                var  code = $"Return {i} + {i}";
                var result = VisualBasicScript.Evaluate<int>(code);

                Assert.Equal(i + i, result);
            }
        }
    }
}
