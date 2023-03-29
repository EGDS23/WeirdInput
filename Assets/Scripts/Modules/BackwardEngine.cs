using UnityEngine;

public class BackwardEngine : Module
{
    public override void Action()
    {
        ship.transform.Translate(Vector3.down * Time.deltaTime * constant);
    }
}
