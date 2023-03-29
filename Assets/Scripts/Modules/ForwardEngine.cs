using UnityEngine;

public class ForwardEngine : Module
{
    public AudioClip engine;
    private ParticleSystem[] _engineVFXs;
    
    private void Awake()
    {
        _engineVFXs = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        foreach (var vfx in _engineVFXs)
        {
            vfx.Stop();
        }
    }
    public override void Action()
    {
        ship.transform.Translate(Vector3.up * Time.deltaTime * constant);
        /*
         if (!source.isPlaying)
        {
            source.clip = engine;
            source.Play();
        }
         */
        foreach (var vfx in _engineVFXs)
        {
            if (!vfx.isEmitting)
            {
                vfx.Play();
            }
        }
    }
}
