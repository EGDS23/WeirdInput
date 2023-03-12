using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Module : MonoBehaviour
{
    public ModuleType type; 

    // key mapping of module
    public KeyCode control;
    public GameObject ship;
    public Vector3 pos;
    public Quaternion rot;
    public float constant = 5;

    private void Start() {
        ResetPos();
    }

    public void ResetPos(){
        var tmp = this.transform.parent;
        if(tmp != null && tmp.gameObject.CompareTag("ship")) ship = tmp.gameObject;
        if(ship != null) {
            this.transform.localPosition = pos;
            this.transform.localRotation = rot;
        }
    }

    private void Update() {
        if(Input.GetKey(control)) Action();
    }

    public abstract void Action();
}
