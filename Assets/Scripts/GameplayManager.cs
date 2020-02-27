using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private void Start()
    {
        int player = GetRandomPlayer();
        Debug.Log("RandomPlayer: " + player.ToString());

        // Initialization of the state machine
        GameplayState gs = new GameplayState();
        if (player == 1)
        {
            // Transition to PlayerTurnState
            gs.actualGameplayState.ToPlayerTurnState();
        }
        else
        {
            // Transition to EnemyTurnState
            gs.actualGameplayState.ToEnemyTurnState();
        }

        Debug.Log("Turn: " + gs.actualGameplayState.ToString());
    }

    private int GetRandomPlayer()
    {
        // Return a random integer number between 1 [inclusive] and 3 [exclusive]
        return Random.Range(1, 3);
    }

}

