using System;
using System.Collections.Generic;

public static class InsultFiller
{
    public static InsultNode[] FillInsults()
    {
        var insults = new List<InsultNode>();
        insults.Add(CreateNode(
            "This is the END for you, you gutter-crawling cur!",
            "And I’ve got a little TIP for you, get the POINT?"));

        insults.Add(CreateNode(
            "Soon you’ll be wearing my sword like a shish kebab!",
            "First you better stop waiving it like a feather-duster."));

        insults.Add(CreateNode(
            "My handkerchief will wipe up your blood!",
            "So you got that job as janitor, after all."));

        insults.Add(CreateNode(
            "People fall at my feet when they see me coming.",
            "Even BEFORE they smell your breath?"));

        insults.Add(CreateNode(
            "I once owned a dog that was smarter then you.",
            "He must have taught you everything you know."));

        return insults.ToArray();
    }

    private static InsultNode CreateNode(string insult, string answer)
    {
        var node = new InsultNode
        {
            Insult = insult,
            Answer = answer
        };
        return node;
    }

}