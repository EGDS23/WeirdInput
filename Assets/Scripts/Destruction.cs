using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Invoke("_destroy", 2f);
    }

    private void _destroy()
    {
        Destroy(gameObject);
    }
 
}
