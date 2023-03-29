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
    public int health = 3;

    public delegate void OnDestroyedHandler(Module mod);
    public event OnDestroyedHandler OnDestroyed;

    
    public delegate void OnDamageHandler(Module mod);
    public event OnDamageHandler OnHealthChange;

    public AudioSource source;
    public AudioClip hit;

    public void Start() {
        source = GetComponent<AudioSource>();
        ResetPos();
    }

    public virtual void ResetPos(){
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

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("enemy")){
            health -= 1;
            if(OnHealthChange != null) OnHealthChange(this);
            Debug.Log("Module " + type + " hit!");
            source.clip = hit;
            source.Play();
        }    
        if(health <= 0) Destroy(this.gameObject);
    }

    private void OnDestroy() {
        if(OnDestroyed != null) OnDestroyed(this);
    }

    public abstract void Action();
}
