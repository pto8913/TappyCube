using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnOutOfContainer : UnityEvent<int> { }

public enum OutOfArea
{
    Respawn,
    Destroy,
}

public interface IObstacleSpikeContainer
{
    public void InvokeOutOfContainer(int index);
}

public class Obstacle_Spike_Container : MonoBehaviour, IObstacleSpikeContainer
{
    public OnOutOfContainer onOutOfContainer = new OnOutOfContainer();

    public Vector3 betweenDistance = new Vector3(3, 0, 0);
    public OutOfArea outOfAreaProcess = OutOfArea.Respawn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            IObstacleMovement childInterface = transform.GetChild(i).GetComponent<IObstacleMovement>();
            childInterface.SetIndex(i);
        }
    }

    public void InvokeOutOfContainer(int index)
    {
        switch (outOfAreaProcess)
        {
            case OutOfArea.Respawn:
                if (index == 0)
                {
                    transform.GetChild(index).transform.position = transform.GetChild(transform.childCount - 1).transform.position + betweenDistance;
                }
                else
                {
                    transform.GetChild(index).transform.position = transform.GetChild(index - 1).transform.position + betweenDistance;
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
