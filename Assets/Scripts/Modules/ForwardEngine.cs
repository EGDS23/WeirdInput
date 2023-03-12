using UnityEngine;

public class ForwardEngine : Module
{
    public override void Action()
    {
        ship.transform.Translate(Vector3.up * Time.deltaTime * constant);
    }
}
