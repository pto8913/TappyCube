using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public interface IItem
{
    public bool Pickup(GameObject go);
    public void Attach(GameObject player);
    public ItemType GetItemType();
}

public class Item : MonoBehaviour, IItem
{
    [SerializeField] public string playerTag = "Player";
    [SerializeField] public ItemType type;

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
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public bool Pickup(GameObject obj)
    {
        if (GameManager.Instance.GetGameState() != GameState.Progress) return false;

        if (obj.CompareTag(playerTag))
        {
            IPlayer player = obj.GetComponent<IPlayer>();
            if (player.HasItem(type))
            {
                Destroy(gameObject);
                return false;
            }
            return true;
        }
        return false;
    }
    
    public void Attach(GameObject player)
    {
        if (GameManager.Instance.GetGameState() != GameState.Progress) return;

#if UNITY_EDITOR
        Debug.Log("Attach");
#endif
        transform.SetParent(player.transform, false);
        transform.localPosition = Vector3.zero;
        transform.position = player.transform.position;
        transform.localScale *= 1.5f;

        Destroy(gameObject.GetComponent<ItemMovement>());
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }

    public ItemType GetItemType()
    {
        return type;
    }
}
