using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModuleType
{
    RightWing       = 0,    // 0000 0001
    LeftWing        = 1,    // 0000 0010
    Weapon          = 2,    // 0000 0100
    Special         = 4,    // 0000 1000
    EngineForward   = 8,    // 0001 0000
    EngineBackward  = 16,   // 0010 0000
}


public class SpaceShip : MonoBehaviour
{
    public Queue<Module> modules;
    int moduleMask = 0;
    float retireTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        modules = new Queue<Module>();
        Module[] mods = GetComponentsInChildren<Module>();
        foreach (Module m in mods)
        {
            if((moduleMask & (int)m.type) != 0){
                Debug.Log("Module " + m.type + " already exists!");
                Destroy(m.gameObject);
                continue;
            }

            m.ResetPos();
            modules.Enqueue(m);
            moduleMask |= (int)m.type;
        }

        retireTimer = Random.Range(10, 25);
    }

    void Update()
    {
        retireTimer -= Time.deltaTime;
        if(retireTimer <= 0){
            retireTimer = Random.Range(10, 25);
            Debug.Log("Retiring module");
            RetireModule();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("module")) CollectModule(other.gameObject);
        Debug.Log("Collision with " + other.gameObject.name);
    }

    private void CollectModule(GameObject m){
        Module mod = m.GetComponent<Module>();
        if(mod == null) return;

        // check if module already exists
        if((moduleMask & (int)mod.type) != 0){
            Debug.Log("Module " + mod.type + " already exists!");
            // TODO: maybe add to inventory
            Destroy(m);
            return;
        }

        mod.transform.parent = this.transform;
        mod.GetComponent<Collider2D>().enabled = false;
        mod.ResetPos();
        modules.Enqueue(mod);
        moduleMask |= (int)mod.type;
    }

    private void RetireModule(){
        if(modules.Count == 0) return;

        Module m = modules.Dequeue();
        moduleMask &= ~(int)m.type;
        Destroy(m.gameObject);
    }
}
