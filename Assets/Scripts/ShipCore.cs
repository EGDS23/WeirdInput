using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCore : MonoBehaviour
{
    public SpaceShip ship;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy"){
            ship.EndGame();
        }
    }
}
