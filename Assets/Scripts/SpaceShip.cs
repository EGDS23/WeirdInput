using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public List<Module> modules;
    
    // Start is called before the first frame update
    void Start()
    {
        Module[] mods = this.GetComponents<Module>();
        foreach (Module m in mods)
        {
            modules.Add(m);
        }
    }

}
