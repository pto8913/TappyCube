using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputActions : MonoBehaviour
{
    public float jumpPower = 16f;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StopMovementImmediately();
        GameManager.Instance.onGameStateChanged.AddListener(GameStateChanged);
        GameStateChanged(GameManager.Instance.GetGameState());
    }
    void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameOver:
                StopMovementImmediately();
                break;
            case GameState.Title:
                StopMovementImmediately();
                break;
            case GameState.Progress:
                RestartMovement();
                break;
            default:
                break;
        }
    }

    private void StopMovementImmediately()
    {
        enabled = false;
    }
    private void RestartMovement()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;

        if (Input.GetButtonDown("Jump"))// && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
}