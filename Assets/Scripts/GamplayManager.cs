using UnityEngine;

public class GamplayManager : MonoBehaviour
{
    private void Start()
    {
        int player = GetRandomPlayer();
        Debug.Log("RandomPlayer: " + player.ToString());

        //currentNode = StoryFiller.FillStory();
        //HistoryText.text = string.Empty;
        //FillUi();
    }

    private int GetRandomPlayer()
    {
        // Return a random integer number between 1 [inclusive] and 3 [exclusive]
        return Random.Range(1, 3);
    }

}
