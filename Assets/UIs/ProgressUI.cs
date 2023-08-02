using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    public void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(GameStateChanged);
        GameStateChanged(GameManager.Instance.GetGameState());
    }

    public void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Progress:
                GetComponent<Canvas>().enabled = true;
                enabled = true;
                break;
            default:
                GetComponent<Canvas>().enabled = false;
                enabled = false;
                break;
        }
    }

    public void Update()
    {
        if (!enabled) return;

        if (scoreText)
        {
            scoreText.text = "score : " + GameManager.Instance.GetScore().ToString();
        }
    }
}