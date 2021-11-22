using ExpressionEvaluation.Shared;

namespace ExpressionEvaluation.Operations
{
    public class DivisionHandler:AbstractHandler
    {
        private IEvaluator _eval;
        public DivisionHandler(IEvaluator eval)
        {
            _eval = eval;
        }
        public override object Handle(string input)
        {
            string evaluatedDivisionExpression = _eval.EvalExpression('/', input);
            return base.Handle(evaluatedDivisionExpression);
        }
    }
}
