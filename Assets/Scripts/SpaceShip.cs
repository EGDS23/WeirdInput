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
    [SerializeField] ShipCore core;
    [SerializeField] GameObject shipUI;

    Dictionary<ModuleType, ModuleUI> moduleUIs;
    public List<GameObject> inventory;
    public List<Module> modules;
    int moduleMask = 0;
    int moduleLayer = 7;
    float retireTimer = 0;

    public GameObject explosion;
    AudioSource source;
    public AudioClip retireclip;
    public AudioClip installclip;
    // Start is called before the first frame update
    void Start()
    {
        moduleUIs = new Dictionary<ModuleType, ModuleUI>();
        ModuleUI[] ui = shipUI.GetComponentsInChildren<ModuleUI>();
        foreach (ModuleUI m in ui)
        {
            if(moduleUIs.ContainsKey(m.type)) Debug.LogError("ModuleUI " + m.type + " already exists!");
            moduleUIs.Add(m.type, m);
            m.UpdateInventory(0);
            m.OnReplacement += OnReplaceModule;
        }

        modules = new List<Module>();
        Module[] mods = GetComponentsInChildren<Module>();
        foreach (Module m in mods)
        {
            LoadModule(m);
        }

        inventory = new List<GameObject>();
        retireTimer = Random.Range(10, 25);
        source = GetComponent<AudioSource>();
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

        LoadModule(mod);
    }

    private void RetireModule(){
        if(modules.Count == 0) return;

        Module m = modules[0];
        Destroy(m.gameObject);
        source.clip = retireclip;
        source.Play();
    }

    void OnModuleDestroyed(Module m){
        moduleMask &= ~(int)m.type;
        modules.Remove(m);

        if(moduleUIs.ContainsKey(m.type))
            moduleUIs[m.type].UpdateModule(KeyCode.None, 0);

        Instantiate(explosion);
        Debug.Log("Module " + m.type + " destroyed!");
    }

    void OnHealthChange(Module m){
        moduleUIs[m.type].UpdateHealthBar((int)m.health);
    }

    void OnReplaceModule(ModuleType type){
        for(int i = 0; i < inventory.Count; i++){
            if(inventory[i].GetComponent<Module>().type == type){
                GameObject g = inventory[i];
                g.SetActive(true);
                inventory.RemoveAt(i);
                LoadModule(g.GetComponent<Module>());
                return;
            }
        }
    }

    void LoadModule(Module mod){
        // check if module already exists
        if((moduleMask & (int)mod.type) != 0){
            Debug.Log("Module " + mod.type + " already exists!");
            // TODO: maybe add to inventory
            inventory.Add(mod.gameObject);
            if(moduleUIs.ContainsKey(mod.type))
                moduleUIs[mod.type].UpdateInventory(1);
            mod.gameObject.SetActive(false);
            return;
        }

        if(modules.Count == 0) retireTimer = Random.Range(10, 25);

        mod.gameObject.layer = moduleLayer;
        mod.transform.parent = this.transform;
        modules.Add(mod);
        mod.ResetPos();
        mod.OnDestroyed += OnModuleDestroyed;
        mod.OnHealthChange += OnHealthChange;
        moduleMask |= (int)mod.type;

        source.clip = installclip;
        source.Play();

        if(moduleUIs.ContainsKey(mod.type))
            moduleUIs[mod.type].UpdateModule(mod.control, mod.health);
    }
}
