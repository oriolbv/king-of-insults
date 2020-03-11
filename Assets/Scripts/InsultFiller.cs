using UnityEngine;

public static class InsultFiller
{
    public static InsultNode[] FillInsults()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("insults");
        var insults = JsonUtility.FromJson<InsultsSet>(jsonFile.ToString());
        return insults.Insults;
    }
}