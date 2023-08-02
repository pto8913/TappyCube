using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(GameStateChanged);
        GameStateChanged(GameManager.Instance.GetGameState());
    }

    public void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Title:
                GetComponent<Canvas>().enabled = true;
                enabled = true;
                break;
            default:
                GetComponent<Canvas>().enabled = false;
                enabled = false;
                break;
        }
    }

    public void OnClickedStart()
    {
        if (enabled)
        {
#if UNITY_EDITOR
            Debug.Log("Title OnClickedStart");
#endif
            GameManager.Instance.SetGameState(GameState.Progress);
            gameObject.GetComponent<Canvas>().enabled = false;
            enabled = false;
        }
    }

    public void OnClickedExit()
    {
        if (enabled)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
