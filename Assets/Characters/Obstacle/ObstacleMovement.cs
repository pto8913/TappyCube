using UnityEngine;



public interface IObstacleMovement
{
    public void SetIndex(int index);
    public int GetIndex();
}

public class ObstacleMovement : MonoBehaviour, IObstacleMovement
{
    public float speed = 1f;
    [SerializeField] private UnityEngine.Object trigger;

    private int thisIndex = -1;

    public GameObject GetParent()
    {
        return gameObject.transform.parent.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(GameStateChanged);
        GameStateChanged(GameManager.Instance.GetGameState());
    }

    public void SetIndex(int index)
    {
        thisIndex = index;
    }
    public int GetIndex()
    {
        return thisIndex;
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

        transform.Translate(Vector3.left * Time.deltaTime * speed * GameManager.Instance.GetGlobalTimeDilation());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
#if UNITY_EDITOR
        //Debug.Log(collision.gameObject.name);
#endif
        if (collision.gameObject.Equals(trigger))
        {
            IObstacleSpikeContainer parent = GetParent().GetComponent<IObstacleSpikeContainer>();
            parent.InvokeOutOfContainer(thisIndex);
        }
    }
}
