using UnityEngine;

public class Item_Jet : Item
{
    [SerializeField] private GameObject _jet;
    public override GameObject Attach(GameObject player)
    {
        if (GameManager.Instance.GetGameState() != GameState.Progress)
        {
            return null;
        }
#if UNITY_EDITOR
        Debug.Log("Attach Item Jet Active");
#endif
        GameObject jet_active = Instantiate(_jet);
        jet_active.transform.SetParent(player.transform, false);
        jet_active.transform.localPosition = Vector3.zero;
        jet_active.transform.position = player.transform.position;
        jet_active.transform.localScale *= 1.5f;

        Destroy(gameObject);

        return jet_active;
    }
}
