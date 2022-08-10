using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRenderer : MonoBehaviour
{
    public class SamplePoint
    {
        public Vector3 position;
        public float value;
    }

    [SerializeField] protected PointCloudRenderer pointCloudRenderer;

    [SerializeField] protected float particalAlpha = 1;
    [SerializeField] protected float minSize, maxSize;

    [SerializeField] protected float dataFrequencyMeters;
    [SerializeField] protected float positionalInaccuracy;

    protected List<DataSource> dataSources = new List<DataSource>();

    public virtual void RegisterDataSource(DataSource dataSource)
    {
        dataSources.Add(dataSource);
    }

    public virtual void RenderData(SamplePoint[] dataSources)
    {
        PointCloudRenderer.PointData[] particleData = new PointCloudRenderer.PointData[dataSources.Length];

        float maxValue = float.MinValue;
        float minValue = float.MaxValue;
        foreach (var point in dataSources)
        {
            minValue = Mathf.Min(minValue, point.value);
            maxValue = Mathf.Max(maxValue, point.value);
        }


        int i = 0;
        foreach (var point in dataSources)
        {
            var normalizeVal = (point.value - minValue) / (maxValue - minValue);
            particleData[i] = new PointCloudRenderer.PointData() { position = point.position, color = new Color(normalizeVal, 1 - normalizeVal, 0, particalAlpha), size = Mathf.Lerp(minSize, maxSize, normalizeVal) };
            maxValue = Mathf.Max(maxValue, point.value);
            i++;
        }

        pointCloudRenderer.RenderParticleSystemWithData(particleData);
    }
}
