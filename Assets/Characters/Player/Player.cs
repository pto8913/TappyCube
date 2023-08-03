using UnityEngine;
using Util;

public interface IPlayer
{
    public bool HasItem(ItemType type);
}

public class Player : MonoBehaviour, IPlayer
{
    private GameObject go_Barrier;
    private GameObject go_Jet;
    private GameObject lastHitDeath;
    private GameObject lastHitScore;

    [SerializeField] private LayerMask deathLayer;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private AudioSource SE_Barrier_Broke;

    private void Update()
    {
        if (!enabled) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
    public GameObject GetParent()
    {
        return gameObject.transform.parent.gameObject;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) return;

        if (UtilImpl.IsSameLayer(collision.gameObject.layer, deathLayer))
        {
            if (lastHitDeath == collision.gameObject) return;
            lastHitDeath = collision.gameObject;

            if (go_Jet != null)
            {
#if UNITY_EDITOR
                Debug.Log("has go_Jet");
#endif
                return;
            }

            if (go_Barrier != null)
            {
#if UNITY_EDITOR
                Debug.Log("has go_Barrier");
#endif
                SE_Barrier_Broke.Play();
                Destroy(go_Barrier);
                return;
            }

            GameManager.Instance.SetGameState(GameState.GameOver);
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
                switch (item.GetItemType())
                {
                    case ItemType.Barrier:
                        go_Barrier = item.Attach(gameObject);
                        break;
                    case ItemType.Jet:
                        go_Jet = item.Attach(gameObject);
#if UNITY_EDITOR
                        Debug.Log("Set go_Jet");
#endif
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (UtilImpl.IsSameLayer(collision.gameObject.layer, deathLayer))
        {
            if (lastHitScore == collision.gameObject) return;
            lastHitScore = collision.gameObject;

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
            case ItemType.Jet:
                if (go_Jet != null)
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
