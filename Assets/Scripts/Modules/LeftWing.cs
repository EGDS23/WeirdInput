using UnityEngine;

public class LeftWing : Module
{
    public override void Action()
    {
        ship.transform.Rotate(Vector3.forward * constant * Time.deltaTime);
    }
}
