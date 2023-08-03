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
public class OnResetSpike : UnityEvent { }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState currentGameState;
    public OnGameStateChanged onGameStateChanged = new OnGameStateChanged();

    private float currentTimeDilation = 1f;
    [SerializeField] private int spawnItemPerObject = 2;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SE_GameOver;

    private int score = 0;
    private int lowQuarityStart = 0;

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
                BGM.Play();
                break;
            case GameState.GameOver:
                BGM.Stop();
                SE_GameOver.Play();
                /* For less skillful players */
                if (score < 10)
                {
                    ++lowQuarityStart;
                    if (lowQuarityStart >= 3)
                    {
                        spawnItemPerObject /= 2;
                        lowQuarityStart = 0;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void SetGlobalTimeDilation(float inTime)
    {
        currentTimeDilation = inTime;
    }
    public float GetGlobalTimeDilation()
    {
        return currentTimeDilation;
    }

    public void AddScore(int inScore)
    {
        score += inScore;
        if (score % spawnItemPerObject == 0)
        {
            ItemManager.Instance.SpawnItem((ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length));
        }
    }
    public int GetScore()
    {
        return score;
    }
}
