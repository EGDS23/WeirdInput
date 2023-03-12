using UnityEngine;

public class RightWing : Module
{
    public override void Action()
    {
        ship.transform.Rotate(Vector3.back * constant * Time.deltaTime);
    }
}
