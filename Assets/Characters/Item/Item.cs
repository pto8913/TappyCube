using UnityEngine;

public interface IItem
{
    public bool Pickup(GameObject go);
    public GameObject Attach(GameObject player);
    public ItemType GetItemType();
}

public class Item : MonoBehaviour, IItem
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private ItemType type;

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

    public virtual GameObject Attach(GameObject player)
    {
        if (GameManager.Instance.GetGameState() != GameState.Progress)
        {
            return null;
        }
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

        return gameObject;
    }

    public ItemType GetItemType()
    {
        return type;
    }
}
