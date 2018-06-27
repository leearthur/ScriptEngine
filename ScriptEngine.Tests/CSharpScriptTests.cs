using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScriptEngine.Tests
{
    public class CSharpScriptTests
    {
        [Fact]
        public void Evaluate_SimpleArithmeticOperation_ResultReturned()
        {
            const string code = "1 + 1";
            var result = CSharpScript.Evaluate<int>(code);

            Assert.Equal(2, result);
        }

        [Fact]
        public void Evaluate_MultipleSimpleArithmeticOperation_ResultsReturned()
        {
            for (var i = 0; i < 250; i++)
            {
                var code = $"{i} + {i}";
                var result = CSharpScript.Evaluate<int>(code);

                Assert.Equal(i + i, result);
            }
        }
    }
}
