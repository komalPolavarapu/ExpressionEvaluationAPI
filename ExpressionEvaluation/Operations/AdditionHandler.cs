using ExpressionEvaluation.Shared;

namespace ExpressionEvaluation.Operations
{
    public class AdditionHandler:AbstractHandler
    {
        private IEvaluator _eval;
        public AdditionHandler(IEvaluator eval)
        {
            _eval = eval;
        }
        public override object Handle(string input)
        {
            string evaluatedAdditionExpression = _eval.EvalExpression('+', input);
            return base.Handle(evaluatedAdditionExpression);
        }
    }
}
