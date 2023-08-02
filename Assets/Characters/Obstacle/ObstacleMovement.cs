using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum OutOfArea
{
    Respawn,
    Destroy,
}

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 restartPos = new Vector3(9, 0, 0);

    [SerializeReference] private UnityEngine.Object trigger;
    [SerializeReference] private OutOfArea outOfAreaProcess = OutOfArea.Respawn;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(GameStateChanged);
        GameStateChanged(GameManager.Instance.GetGameState());
    }

    void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameOver:
                enabled = false;
                break;
            case GameState.Title:
                enabled = false;
                break;
            case GameState.Progress:
                enabled = true;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
#if UNITY_EDITOR
        //Debug.Log(collision.gameObject.name);
#endif
        switch (outOfAreaProcess)
        {
            case OutOfArea.Respawn:
                if (collision.gameObject.Equals(trigger))
                {
                    transform.position = restartPos;
                }
                break; 
            case OutOfArea.Destroy:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
