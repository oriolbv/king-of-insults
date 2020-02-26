using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    // Different gameplay states
    [HideInInspector] public IActualGameplayState actualGameplayState;
    [HideInInspector] public PlayerTurnState playerTurnState;
    [HideInInspector] public EnemyTurnState enemyTurnState;
    [HideInInspector] public EndGameState endGameState;
}
public interface IActualGameplayState
{
    IActualGameplayState GetActualState();

    void ToPlayerTurnState();

    void ToEnemyTurnState();

    void ToEndGameState();
}

public class PlayerTurnState : IActualGameplayState
{
    private readonly GameplayState gameplayState = null;

    public IActualGameplayState GetActualState()
    {
        return gameplayState.playerTurnState;
    }

    public void ToPlayerTurnState()
    {
        Debug.Log("You're already in Player turn!");
    }

    public void ToEnemyTurnState()
    {
        gameplayState.actualGameplayState = gameplayState.enemyTurnState;
    }

    public void ToEndGameState()
    {
        gameplayState.actualGameplayState = gameplayState.endGameState;
    }
}

public class EnemyTurnState : IActualGameplayState
{
    private readonly GameplayState gameplayState = null;

    public IActualGameplayState GetActualState()
    {
        return gameplayState.enemyTurnState;
    }

    public void ToPlayerTurnState()
    {
        gameplayState.actualGameplayState = gameplayState.playerTurnState;
    }

    public void ToEnemyTurnState()
    {
        Debug.Log("You're already in Enemy turn!");
    }

    public void ToEndGameState()
    {
        gameplayState.actualGameplayState = gameplayState.endGameState;
    }
}

public class EndGameState : IActualGameplayState
{
    private readonly GameplayState gameplayState = null;

    public IActualGameplayState GetActualState()
    {
        return gameplayState.endGameState;
    }

    public void ToPlayerTurnState()
    {
        gameplayState.actualGameplayState = gameplayState.playerTurnState;
    }

    public void ToEnemyTurnState()
    {
        gameplayState.actualGameplayState = gameplayState.enemyTurnState;
    }

    public void ToEndGameState()
    {
        Debug.Log("You're already in Player turn!");

    }
}
