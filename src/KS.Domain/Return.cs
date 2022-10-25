namespace KS.Domain;

public class Return<TResult, TError> where TError : Exception
{
    TResult Result { get; }
    TError Error { get; }

    public Return(TResult result, TError error)
    {
        Result = result;
        Error = error;
    }
}
