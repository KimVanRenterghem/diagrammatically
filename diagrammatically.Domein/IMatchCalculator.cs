﻿namespace diagrammatically.Domein
{
    public interface IMatchCalculator
    {
        double Calculate(string filter, string match);
        char Replace(char toReplace);
    }
}