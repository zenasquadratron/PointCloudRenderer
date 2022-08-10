using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedDataSource : DataSource
{
    public float radius;
    public float intensity;
    public Transform origin;

    public Vector3 center { get { return origin.position; } }

    private void Awake()
    {
        SimulatedDataRenderer.Instance.RegisterSimulatedDataSource(this);
    }
}
