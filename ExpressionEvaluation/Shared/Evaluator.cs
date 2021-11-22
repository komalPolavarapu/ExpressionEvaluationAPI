using System;
using System.Linq;
using System.Web.Http;

namespace ExpressionEvaluation.Shared
{
    public class Evaluator : IEvaluator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchOperator"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string EvalExpression(char searchOperator, string input)
        {
            try
            {
                int operatorndex = input.IndexOf(searchOperator);
                char[] operators = new char[] { '*', '/', '+', '-' };

                if (operatorndex > 0)
                {
                    int previousOperatorIndex = input.Substring(0, operatorndex).LastIndexOfAny(operators);
                    double value1 = Convert.ToDouble(input.Substring(previousOperatorIndex + 1, operatorndex - (previousOperatorIndex + 1)));

                    int nextOperatorIndex = input.Substring(operatorndex + 1, input.Length - 1 - operatorndex).IndexOfAny(operators);
                    if (nextOperatorIndex < 0)
                    {
                        nextOperatorIndex = input.Length - (operatorndex + 1);
                    }
                    double value2 = Convert.ToDouble(input.Substring(operatorndex + 1, nextOperatorIndex));

                    double result = 0;
                    if (searchOperator == '*')
                        result = value1 * value2;
                    else if (searchOperator == '/')
                        result = value1 / value2;
                    else if (searchOperator == '+')
                        result = value1 + value2;
                    else if (searchOperator == '-')
                        result = value1 - value2;

                    input = input.Replace(value1.ToString() + searchOperator + value2.ToString(), result.ToString());

                    input = EvalExpression(searchOperator, input);
                }

                return input;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// Validate expression to check for non-numeric and non-operators
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ValidateExpression(string input)
        {
            bool isValid = true;
            
            int inputLength = input.Length;

            if (input.Length == 0 || !(input[0] >= 48 && input[0] <= 57) || !(input[inputLength - 1] >= 48 && input[inputLength - 1] <= 57))
                return "Invalid Input - The expression is either blank or starts/ends with non-numeric characters";

            for (int i = 1; i < inputLength - 1; i++)
            {
                isValid = (input[i] >= 48 && input[i] <= 57) // 0-9                         
                          || input[i] == '*'
                          || input[i] == '/'
                          || input[i] == '+'
                          || input[i] == '-';

                if (!isValid) return "Invalid Input - The expression contains non-numeric and non-operator character";
            }

            return null;
        }

        /// <summary>
        /// remove white spaces from the input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string RemoveWhiteSpaces(string input)
        {            
            return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// Handle the input
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public  string HandleInput(AbstractHandler handler, string input)
        {
            try
            {
                var result = handler.Handle(input);

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return "unable to process the request";
                }
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
        }
            
    }
}
