using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private void Start()
    {
        int player = GetRandomPlayer();
        Debug.Log("RandomPlayer: " + player.ToString());

        GameplayState gs = new GameplayState();
        //currentNode = StoryFiller.FillStory();
        //HistoryText.text = string.Empty;
        //FillUi();
        Debug.Log("Actual State: " + gs.actualGameplayState.ToString());
    }

    private int GetRandomPlayer()
    {
        // Return a random integer number between 1 [inclusive] and 3 [exclusive]
        return Random.Range(1, 3);
    }

}

