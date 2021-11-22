using ExpressionEvaluation.Shared;

namespace ExpressionEvaluation.Operations
{
    public class SubtractionHandler:AbstractHandler
    {
        private IEvaluator _eval;
        public SubtractionHandler(IEvaluator eval)
        {
            _eval = eval;
        }
        public override object Handle(string input)
        {
            string evaluatedSubtractionExpression = _eval.EvalExpression('-', input);
            return base.Handle(evaluatedSubtractionExpression);
        }
    }
}
