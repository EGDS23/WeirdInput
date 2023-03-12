using UnityEngine;

public class Special : Module
{
    public override void Action()
    {
        ship.transform.Rotate(Vector3.back * constant * Time.deltaTime);
    }
}
