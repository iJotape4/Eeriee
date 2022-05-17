using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDispeler : MonoBehaviour
{
    private SkinnedMeshRenderer _rend;

    private void Awake()
    {
        _rend = GetComponent<SkinnedMeshRenderer>();
    }

    public void UpdateMaterialsArray()
    {
        Material[] _materials = _rend.materials;
        Array.Reverse(_materials);
        Array.Resize(ref _materials, 1);
        _rend.materials = _materials;

        StartCoroutine(Dispeling( _rend.material));      
    }

    public IEnumerator Dispeling(Material m)
    {
        while(m.color.a !> 0)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, (m.color.a - 1f/255f));
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject.transform.parent.gameObject);
        yield return null;
    }
}

