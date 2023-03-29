using UnityEngine;

public class ForwardEngine : Module
{
    public AudioClip engine;
    public override void Action()
    {
        ship.transform.Translate(Vector3.up * Time.deltaTime * constant);
        if (!source.isPlaying)
        {
            source.clip = engine;
            source.Play();
        }
    }
}
