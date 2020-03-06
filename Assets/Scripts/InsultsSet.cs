using System;
using System.Collections.Generic;

[System.Serializable]
public class InsultsSet
{
    public InsultNode[] Insults;
}

[System.Serializable]
public class InsultNode
{
    public string Insult;
    public string Answer;
}


