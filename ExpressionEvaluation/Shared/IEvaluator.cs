namespace ExpressionEvaluation.Shared
{
    public interface IEvaluator
    {
        public string EvalExpression(char searchOperator, string input);

        public string ValidateExpression(string input);

        public string RemoveWhiteSpaces(string input);

        public string HandleInput(AbstractHandler handler, string input);
    }
}