using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudRenderer : MonoBehaviour
{
    public class PointData
    {
        public Vector3 position;
        public Color color;
        public float size;
    }

    [SerializeField] private new ParticleSystem particleSystem;

    [SerializeField] private float particalAlpha = 1;
    [SerializeField] private float minSize, maxSize;

    private ParticleSystem.Particle[] particles;

    public void RenderParticleSystemWithData(PointData[] data)
    {
        particles = new ParticleSystem.Particle[data.Length];

        int i = 0;
        foreach (PointData pd in data)
        {
            particles[i].color = pd.color;
            particles[i].position = pd.position;
            particles[i].size = pd.size;
            i++;
        }

        particleSystem.SetParticles(particles, particles.Length);
    }
}

