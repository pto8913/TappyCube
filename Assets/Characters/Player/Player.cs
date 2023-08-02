using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public interface IPlayer
{
    public bool HasItem(ItemType type);
}

public class Player : MonoBehaviour, IPlayer
{
    private GameObject go_Barrier;

    [SerializeField] private LayerMask deathLayer;
    [SerializeField] private LayerMask itemLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) return;

        if (UtilImpl.IsSameLayer(collision.gameObject.layer, deathLayer))
        {
            if (go_Barrier != null)
            {
                Destroy(go_Barrier);
            }
            else
            {
                GameManager.Instance.SetGameState(GameState.GameOver);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UtilImpl.IsSameLayer(collision.gameObject.layer, itemLayer))
        {
            IItem item = collision.gameObject.GetComponent<IItem>();

            bool success = item.Pickup(gameObject);
            if (success)
            {
                if (item.GetItemType() == ItemType.Barrier)
                {
                    item.Attach(gameObject);
                    go_Barrier = collision.gameObject;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (UtilImpl.IsSameLayer(collision.gameObject.layer, deathLayer))
        {
            GameManager.Instance.AddScore(1);
        }
    }

    public bool HasItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Barrier:
                if (go_Barrier != null)
                {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
    }
}
