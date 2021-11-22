using ExpressionEvaluation.Operations;
using ExpressionEvaluation.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ExpressionEvaluation.Controllers
{
    [ApiController]   
    public class ExpressionEvaluationController : ControllerBase
    {            
        private readonly IEvaluator _evaluator;

        public ExpressionEvaluationController(IEvaluator evaluator)
        {
            _evaluator = evaluator;           
        }

        [Route("api/evaluate")]
        [HttpGet]
        public IActionResult Evaluate(string input)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            try
            {                
                string cleanedInput = _evaluator.RemoveWhiteSpaces(input);
                string validationMessage = _evaluator.ValidateExpression(cleanedInput);
                if (!string.IsNullOrWhiteSpace(validationMessage))
                {                                
                    return BadRequest(validationMessage) ;
                }

                MultiplicationHandler multiplicationHandler = new MultiplicationHandler(_evaluator);
                DivisionHandler divisionHandler = new DivisionHandler(_evaluator);
                AdditionHandler additionHandler = new AdditionHandler(_evaluator);
                SubtractionHandler subtractionHandler = new SubtractionHandler(_evaluator);
                OtherHandler otherHandler = new OtherHandler();

                multiplicationHandler.SetNext(divisionHandler).SetNext(additionHandler).SetNext(subtractionHandler).SetNext(otherHandler);

                string result = _evaluator.HandleInput(multiplicationHandler, input);
                return Ok(Convert.ToDouble(result));               
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
