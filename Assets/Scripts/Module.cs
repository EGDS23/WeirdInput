using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour
{
    // Right wing: 0000, Left wing: 0001, Weapon: 0010, Special: 0011, Engine forward: 0100, Engine backward: 0101
    public int type; 

    // key mapping of module
    public char control;

    private void Update() {
        foreach(char c in Input.inputString){
            if(c == control){
                Action();
                break;
            }
        }
    }

    public abstract void Action();
}
