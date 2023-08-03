using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Jet_Active : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float speed = 2f;
    private float startTime = 0f;
    private float timeDuration = 0f;
    [SerializeField] private AudioSource SE_Jet;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.SetGlobalTimeDilation(speed);
        startTime = Time.time;
        SE_Jet.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;

        timeDuration = Time.time - startTime;
        if (timeDuration >= lifetime)
        {
            enabled = false;
            Destroy(gameObject);
            return;
        }

        float lerpAlpha = Util.UtilImpl.MapRange(timeDuration, 0, lifetime, 0, 1);
        GameManager.Instance.SetGlobalTimeDilation(Mathf.Lerp(speed, 5, lerpAlpha));
    }

    private void OnDestroy()
    {
        GameManager.Instance.SetGlobalTimeDilation(1f);
    }
}
