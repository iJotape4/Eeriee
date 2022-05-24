using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumEvent : MonoBehaviour
{

    public EerieController _eerie;
    public InteractableObject _io;
    // Start is called before the first frame update
    void Start()
    {
        _eerie = GameObject.FindGameObjectWithTag("Eerie").GetComponent<EerieController>();
        _io = GetComponent<InteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Instantiate(Resources.Load<GameObject>("Prefabs/SpectrumParent"), transform.position, transform.rotation);
            _eerie.CallSeeBeyond();
        }
    }

    public IEnumerator EerieTutorial()
    {
        yield return new WaitForSeconds(1f);
        _eerie.CallSeeBeyond();
    }

}
