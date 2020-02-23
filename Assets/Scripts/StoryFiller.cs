using System;

public static class StoryFiller
{
    public static StoryNode FillStory()
    {
        var root = CreateNode(
            "Te encuentras en una habitación y no recuerdas nada. Quieres salir",
            new[] {
            "Explorar objetos",
            "Explorar habitación"});

        return root;
    }

    private static StoryNode CreateNode(string history, string[] options)
    {
        var node = new StoryNode
        {
            History = history,
            Answers = options,
            NextNode = new StoryNode[options.Length]
        };
        return node;
    }

}
