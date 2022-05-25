using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class BRGlassDoor : Interactable
    {
        public Animator _openAndClose;
        public string opened = "Opened";

        public void Start()
        {
            _openAndClose = GetComponent<Animator>();
        }

        public override void Interact()
        {
            base.Interact();
            _openAndClose.SetBool(opened, _openAndClose.GetBool(opened) ? false : true);
        }
    }
}
