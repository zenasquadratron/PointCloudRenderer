using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedDataRenderer : DataRenderer
{
    public static SimulatedDataRenderer Instance = null;
    
    protected List<SimulatedDataSource> simulatedDataSources = new List<SimulatedDataSource>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public virtual void RegisterSimulatedDataSource(SimulatedDataSource dataSource)
    {
        simulatedDataSources.Add(dataSource);
        SimulatedDataRenderer.Instance.RenderSimulatedData();
    }

    public SamplePoint[] GenerateAndGetData()
    {
        if (dataFrequencyMeters <= 0)
        {
            Debug.LogError($"Invalid value for data frequency {dataFrequencyMeters}. Returning null.");
            return null;
        }

        var data = new List<SamplePoint>();

        Vector3 min = Vector3.one * float.MaxValue;
        Vector3 max = Vector3.one * float.MinValue;
        foreach (var source in simulatedDataSources)
        {
            var sMin = source.center - Vector3.one * source.radius;
            var sMax = source.center + Vector3.one * source.radius;

            min.x = Mathf.Min(min.x, sMin.x);
            min.y = Mathf.Min(min.y, sMin.y);
            min.z = Mathf.Min(min.z, sMin.z);

            max.x = Mathf.Max(max.x, sMax.x);
            max.y = Mathf.Max(max.y, sMax.y);
            max.z = Mathf.Max(max.z, sMax.z);
        }

        for (float x = min.x; x < max.x; x += dataFrequencyMeters)
        {
            for (float y = min.y; y < max.y; y += dataFrequencyMeters)
            {
                for (float z = min.z; z < max.z; z += dataFrequencyMeters)
                {
                    var point = new SamplePoint();
                    point.position = new Vector3(x, y, z) + Random.insideUnitSphere * positionalInaccuracy;

                    foreach (var source in simulatedDataSources)
                    {
                        var dist = Vector3.Distance(point.position, source.center);
                        point.value += Mathf.Lerp(source.intensity, 0, dist / source.radius);
                    }

                    if (point.value > 0)
                    {
                        data.Add(point);
                    }
                }
            }
        }

        
        return data.ToArray();
    }

    public void RenderSimulatedData()
    {
        RenderData(GenerateAndGetData());
    }
}
