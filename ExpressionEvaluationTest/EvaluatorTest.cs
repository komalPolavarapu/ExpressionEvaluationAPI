using ExpressionEvaluation.Operations;
using ExpressionEvaluation.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ExpressionEvaluationTest
{
    public class EvaluatorTest
    {
        private readonly IEvaluator _evaluator;

        public readonly MultiplicationHandler multiplicationHandler;
        public readonly DivisionHandler divisionHandler;
        public readonly AdditionHandler additionHandler;
        public readonly SubtractionHandler subtractionHandler;
        public readonly OtherHandler otherHandler;

        public EvaluatorTest()
        {
            _evaluator = new Evaluator();
            multiplicationHandler = new MultiplicationHandler(_evaluator);
            divisionHandler = new DivisionHandler(_evaluator);
            additionHandler = new AdditionHandler(_evaluator);
            subtractionHandler = new SubtractionHandler(_evaluator);
            otherHandler = new OtherHandler();

            multiplicationHandler.SetNext(divisionHandler).SetNext(additionHandler).SetNext(subtractionHandler).SetNext(otherHandler);
        }

        [Theory]
        [InlineData("4+5*2","14")]
        [InlineData("4+5/2", "6.5")]
        [InlineData("4+5/2-1", "5.5")]
        [InlineData("5+6/2-1-3*2", "1")]
        public void EvaluateValidExpressionWithoutWhiteSpaces(string input, string expectedResult)
        {
            string actualResult = _evaluator.HandleInput(multiplicationHandler, input);
            Assert.Equal(expectedResult, actualResult);            
        }
        
    }
}
