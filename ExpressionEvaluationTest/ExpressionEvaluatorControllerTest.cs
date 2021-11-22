using ExpressionEvaluation.Controllers;
using ExpressionEvaluation.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ExpressionEvaluationTest
{
    public class ExpressionEvaluatorControllerTest
    {
        public ExpressionEvaluationController _evaluationController;
        private readonly IEvaluator _evaluator;
       
        public ExpressionEvaluatorControllerTest()
        {
            _evaluator = new Evaluator();            
            _evaluationController = new ExpressionEvaluationController(_evaluator);
        }
        [Theory]
        [InlineData("4+5*2", 14)]
        [InlineData("4+5/2", 6.5)]
        [InlineData("4+5/2-1", 5.5)]
        [InlineData("15+16/2-1-3*2", 16)]
        public void EvaluateValidExpressionWithoutWhiteSpaces(string input,double expectedResult)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as OkObjectResult;
            double actualresult = Convert.ToDouble(okObjectresult.Value);
            Assert.Equal(expectedResult, actualresult);
        }

        [Theory]
        [InlineData("4*2*2", 16)]
        [InlineData("25*5*2", 250)]        
        public void EvaluateValidExpressionWithOnlyMultiplicationOperator(string input, double expectedResult)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as OkObjectResult;
            double actualresult = Convert.ToDouble(okObjectresult.Value);
            Assert.Equal(expectedResult, actualresult);
        }

        [Theory]
        [InlineData("5+7+8", 20)]
        [InlineData("35+25+200", 260)]
        public void EvaluateValidExpressionWithOnlyAdditionOperator(string input, double expectedResult)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as OkObjectResult;
            double actualresult = Convert.ToDouble(okObjectresult.Value);
            Assert.Equal(expectedResult, actualresult);
        }

        [Theory]
        [InlineData("16/2/2", 4)]
        [InlineData("100/2/2", 25)]
        public void EvaluateValidExpressionWithOnlyDivisionOperator(string input, double expectedResult)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as OkObjectResult;
            double actualresult = Convert.ToDouble(okObjectresult.Value);
            Assert.Equal(expectedResult, actualresult);
        }


        [Theory]
        [InlineData("   4+5*2", 14)]
        [InlineData("  4+5/2", 6.5)]
        [InlineData("     4+5/2-1", 5.5)]
        [InlineData("   5+6/2-1-3*2", 1)]
        public void EvaluateValidExpressionStartingWithWhiteSpaces(string input, double expectedResult)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as OkObjectResult;
            double actualresult = Convert.ToDouble(okObjectresult.Value);
            Assert.Equal(expectedResult, actualresult);
        }

        [Theory]
        [InlineData("4+5*2  ", 14)]
        [InlineData("4+5/2   ", 6.5)]
        [InlineData("4+5/2-1    ", 5.5)]
        [InlineData("5+6/2-1-3*2     ", 1)]
        public void EvaluateValidExpressionEndingWithWhiteSpaces(string input, double expectedResult)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as OkObjectResult;
            double actualresult = Convert.ToDouble(okObjectresult.Value);
            Assert.Equal(expectedResult, actualresult);
        }


        [Theory]
        [InlineData("4a+5*2")]
        [InlineData("4+5h/2")]
        [InlineData("4+5d/2-1")]
        [InlineData("5+dd6/2-1-3*2")]
        public void EvaluateInValidExpressionStartingAndEndingWithNumberAndContainingWithAlphabets(string input)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as BadRequestObjectResult;
            string  actualresult = (string)okObjectresult.Value;
            Assert.Equal("Invalid Input - The expression contains non-numeric and non-operator character", actualresult);
        }

        [Theory]
        [InlineData("a4+5*2")]
        [InlineData("df4+5/2")]
        [InlineData("4+5/2-1f")]
        [InlineData("5+6/2-1-3*2)")]
        public void EvaluateInValidExpressionStartingOrEndingWithAlphabets(string input)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as BadRequestObjectResult;
            string actualresult = (string)okObjectresult.Value;
            Assert.Equal("Invalid Input - The expression is either blank or starts/ends with non-numeric characters", actualresult);
        }

        [Theory]
        [InlineData("a+5*2")]
        [InlineData("s4+5/2")]
        [InlineData("e4+5/2-1")]
        [InlineData("q5+6/2-1-3*2)")]
        public void EvaluateInvalidExpressionStartingWithAphabetAdEndingWithNumber(string input)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as BadRequestObjectResult;
            string actualresult = (string)okObjectresult.Value;
            Assert.Equal("Invalid Input - The expression is either blank or starts/ends with non-numeric characters", actualresult);

        }

        [Theory]
        [InlineData("+5*2")]
        [InlineData("-4+5/2")]
        [InlineData("+4+5/2-1")]
        [InlineData("-5+6/2-1-3*2)")]
        public void EvaluateInvalidExpressionStartingWithOperatorAndEndingWithNumber(string input)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as BadRequestObjectResult;
            string actualresult = (string)okObjectresult.Value;
            Assert.Equal("Invalid Input - The expression is either blank or starts/ends with non-numeric characters", actualresult);

        }

        [Theory]
        [InlineData("5*2*")]
        [InlineData("4+5/2+")]
        [InlineData("4+5/2-1-")]
        [InlineData("5+6/2-1-3*2/)")]
        public void EvaluateInvalidExpressionStartingWithNumberAndEndingWith(string input)
        {
            var response = _evaluationController.Evaluate(input);
            var okObjectresult = response as BadRequestObjectResult;
            string actualresult = (string)okObjectresult.Value;
            Assert.Equal("Invalid Input - The expression is either blank or starts/ends with non-numeric characters", actualresult);

        }
    }
}
