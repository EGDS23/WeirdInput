using System;
using Unity.VisualScripting;
using UnityEngine;

public class RightWing : Module
{
    private ParticleSystem[] _engineVFX;
    private void Awake()
    {
        _engineVFX = gameObject.GetComponents<ParticleSystem>();
    }

    public override void Action()
    {
        ship.transform.Rotate(Vector3.back * constant * Time.deltaTime);
    }
}
