using System;
using Unity.VisualScripting;
using UnityEngine;

public class RightWing : Module
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
        ship.transform.Rotate(Vector3.forward * constant * Time.deltaTime);
        if (!_engineVFX.isEmitting)
        {
            _engineVFX.Play();
        }
    }
}
