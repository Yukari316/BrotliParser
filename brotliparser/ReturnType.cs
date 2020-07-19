namespace brotliparser
{
    internal enum ReturnType : int
    {
        EmptyFile = 1,
        Success = 0,
        IllegalArgs = -1,
        ParseError = -2,
        IOError = -3
    }
}
