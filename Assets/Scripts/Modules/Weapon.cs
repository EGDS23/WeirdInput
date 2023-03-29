using UnityEngine;
using System.Collections.Generic;

public class Weapon : Module
{
    List<KeyCode> ammos = new List<KeyCode>{KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash};
    public ModuleUI ui;
    private int ammoIndex = 0;
    public GameObject bullet;
    public void Start() {
        if(control == KeyCode.None || !ammos.Contains(control)) {
            control = ammos[Random.Range(0, ammos.Count)];
            ui.UpdateKey(control);
        }
        else {
            ammoIndex = ammos.IndexOf(control);
        }

    }

    public override void Action()
    {
        ammoIndex++;
        if(ammoIndex >= ammos.Count) ammoIndex = 0;
        control = ammos[ammoIndex];

        ui.UpdateKey(control);
        Instantiate(bullet, ship.transform.position, ship.transform.rotation);
    }
}
