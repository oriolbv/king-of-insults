using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState
{
    // Different gameplay states
    [HideInInspector] public IActualGameplayState actualGameplayState;
    [HideInInspector] public StartGameState startGameState;
    [HideInInspector] public PlayerTurnState playerTurnState;
    [HideInInspector] public EnemyTurnState enemyTurnState;
    [HideInInspector] public EndGameState endGameState;

    public GameplayState()
    {
        // Initialization of all states
        startGameState = new StartGameState(this);
        playerTurnState = new PlayerTurnState(this);
        enemyTurnState = new EnemyTurnState(this);
        endGameState = new EndGameState(this);

        // At initialization, we set the actual gameplay state to StartGameState
        actualGameplayState = startGameState;
    }

}
public interface IActualGameplayState
{
    IActualGameplayState GetActualState();

    void ToStartGameState();

    void ToPlayerTurnState();

    void ToEnemyTurnState();

    void ToEndGameState();
}

public class StartGameState : IActualGameplayState
{
    private readonly GameplayState gameplayState = null;

    public StartGameState(GameplayState gs)
    {
        gameplayState = gs;
    }

    public IActualGameplayState GetActualState()
    {
        return gameplayState.startGameState;
    }

    public void ToStartGameState()
    {
        Debug.Log("You're already in Start game!");
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
        gameplayState.actualGameplayState = gameplayState.endGameState;

    }
}

public class PlayerTurnState : IActualGameplayState
{
    private readonly GameplayState gameplayState = null;

    public PlayerTurnState(GameplayState gs)
    {
        gameplayState = gs;
    }
    public IActualGameplayState GetActualState()
    {
        return gameplayState.playerTurnState;
    }

    public void ToStartGameState()
    {
        gameplayState.actualGameplayState = gameplayState.startGameState;
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

    public EnemyTurnState(GameplayState gs)
    {
        gameplayState = gs;
    }

    public IActualGameplayState GetActualState()
    {
        return gameplayState.enemyTurnState;
    }

    public void ToStartGameState()
    {
        gameplayState.actualGameplayState = gameplayState.startGameState;
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

    public EndGameState(GameplayState gs)
    {
        gameplayState = gs;
    }

    public IActualGameplayState GetActualState()
    {
        return gameplayState.endGameState;
    }

    public void ToStartGameState()
    {
        gameplayState.actualGameplayState = gameplayState.startGameState;
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
        Debug.Log("You're already in End game!");

    }
}
