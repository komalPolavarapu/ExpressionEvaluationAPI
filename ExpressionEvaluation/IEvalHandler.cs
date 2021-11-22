
namespace ExpressionEvaluation
{
    public interface IEvalHandler
    {
        IEvalHandler SetNext(IEvalHandler handler);

        object Handle(string input);
    }
}
