using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMarkerController : MonoBehaviour
{

    Light _marker;
    // Start is called before the first frame update
    void Start()
    {
        _marker = GetComponent<Light>();
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Flash()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            _marker.range = _marker.range == 20f ? 100f : 20f;
            yield return new WaitForSeconds(0.3f);
        }
       

        yield return null;
    }
}
