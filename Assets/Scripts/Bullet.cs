using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update(){
        transform.Translate(Vector3.up * Time.deltaTime * 10);
        Invoke("Destroy", 2f);
    }

    void Destroy(){
        Destroy(gameObject);
    }
}
