using System;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float speed = 1f;
    public float scalerUpDown = 0.5f;
    public Vector3 restartPos = new Vector3(9, 0, 0);

    [SerializeReference] private Vector3 OutOfAreaPos;
    [SerializeReference] private OutOfArea outOfAreaProcess = OutOfArea.Destroy;

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
        if (!enabled) return;

        transform.Translate(Vector3.left * Time.deltaTime * speed * GameManager.Instance.GetGlobalTimeDilation() + Vector3.up * (float)Math.Sin(Time.deltaTime) * scalerUpDown);

        if (transform.position == OutOfAreaPos)
        {
            switch (outOfAreaProcess)
            {
                case OutOfArea.Respawn:
                    transform.position = restartPos;
                    break;
                case OutOfArea.Destroy:
                    enabled = false;
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
