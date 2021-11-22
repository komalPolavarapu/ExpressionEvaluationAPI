using ExpressionEvaluation.Shared;

namespace ExpressionEvaluation.Operations
{
    public class MultiplicationHandler : AbstractHandler
    {
        private IEvaluator _eval;
        public MultiplicationHandler(IEvaluator eval)
        {
            _eval = eval;
        }
        public override object Handle(string input)
        {
            string evaluatedMultiplicationExpression = _eval.EvalExpression('*', input);
            return base.Handle(evaluatedMultiplicationExpression);
        }
    }
}
