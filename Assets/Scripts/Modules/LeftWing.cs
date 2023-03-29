using UnityEngine;

public class LeftWing : Module
{
    private ParticleSystem _engineVFX;
    private void Awake()
    {
        _engineVFX = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _engineVFX.Stop();
    }
    
    public override void Action()
    {
        ship.transform.Rotate(Vector3.back * constant * Time.deltaTime);
        if (!_engineVFX.isEmitting)
        {
            _engineVFX.Play();
        }
    }
}
