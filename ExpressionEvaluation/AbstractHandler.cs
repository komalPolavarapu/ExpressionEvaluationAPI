
namespace ExpressionEvaluation
{
    public class AbstractHandler : IEvalHandler
    {
        private IEvalHandler _nextHandler;
        public virtual object Handle(string input)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(input);
            }
            else
            {
                return null;
            }
        }

        public IEvalHandler SetNext(IEvalHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
    }
}
