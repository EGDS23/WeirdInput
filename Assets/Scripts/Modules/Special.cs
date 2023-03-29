using UnityEngine;

public class Special : Module
{
    [SerializeField] int count = 3;
    public override void Action()
    {
        // teleport
        ship.transform.position += Vector3.up * constant;
        count--;

        if(count <= 0) {
            Destroy(gameObject);
        }
    }
}
