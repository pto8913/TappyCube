using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    Jet,
    Barrier,
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public GameObject jetPrefab;
    public GameObject barrierPrefab;

    [SerializeField] public Vector2 SpawnLocationMin;
    [SerializeField] public Vector2 SpawnLocationMax;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnItem(ItemType itemType)
    {
        if (GameManager.Instance.GetGameState() != GameState.Progress) return;

        GameObject newObject = null;
        switch (itemType)
        {
            case ItemType.Jet:
                newObject = Instantiate(jetPrefab);
                break; 
            case ItemType.Barrier:
                newObject = Instantiate(barrierPrefab);
                break; 
            default:
                break;
        }
        if (newObject != null)
        {
            Vector3 SpawnLocation = new Vector3();
            SpawnLocation.x = Random.Range(SpawnLocationMin.x, SpawnLocationMax.x);
            SpawnLocation.y = Random.Range(SpawnLocationMin.y, SpawnLocationMax.y);
            newObject.transform.position = SpawnLocation;
        }
    }
}
