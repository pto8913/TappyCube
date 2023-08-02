using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(GameStateChanged);
        GameStateChanged(GameManager.Instance.GetGameState());
    }

    public void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameOver:
                GetComponent<Canvas>().enabled = true;
                scoreText.text = "score : " + GameManager.Instance.GetScore().ToString();
                enabled = true;
                break;
            default:
                GetComponent<Canvas>().enabled = false;
                enabled = false;
                break;
        }
    }
    public void OnClickedRestart()
    {
        if (!enabled) return;

#if UNITY_EDITOR
        Debug.Log("-----------------------------------------------");
        Debug.Log("GameOverUI OnClickedRestart");
#endif
        SceneManager.LoadScene(0);
        GameManager.Instance.SetGameState(GameState.Progress);
    }

    public void OnClickedExit()
    {
        if (!enabled) return;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
