using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Title,
    Progress,
    GameOver,
}

[System.Serializable]
public class OnGameStateChanged : UnityEvent<GameState> { }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState currentGameState;
    public OnGameStateChanged onGameStateChanged = new OnGameStateChanged();

    private int score = 0;

    private void Awake()
    {
        Instance = this;
        SetGameState(GameState.Title);
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }

    public void SetGameState(GameState state)
    {
        Debug.Log(state.ToString());
        currentGameState = state;
        onGameStateChanged.Invoke(currentGameState);

        switch (state)
        {
            case GameState.Title:
                score = 0;
                break;
            default:
                break;
        }
    }

    public void AddScore(int inScore)
    {
        score += inScore;
        if (score % 2 == 0)
        {
            ItemManager.Instance.SpawnItem(ItemType.Barrier);
        }
    }
    public int GetScore()
    {
        return score;
    }
}
