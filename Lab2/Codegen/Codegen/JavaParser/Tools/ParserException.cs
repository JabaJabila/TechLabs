﻿namespace Codegen.JavaParser.Tools;

public class ParserException : Exception
{
    public ParserException()
    {
    }

    public ParserException(string message) : base(message) {
}
}