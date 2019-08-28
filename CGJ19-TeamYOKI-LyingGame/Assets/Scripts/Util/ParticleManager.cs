using System.Collections;
using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    public Particle[] particles;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Create(string _name, Vector3 _position, Quaternion _rotation)
    {
        Particle p = Array.Find(particles, item => item.name == _name);
        if (p == null)
        {
            Debug.LogWarning("Particle: " + _name + " not found!");
            return;
        }

        Instantiate(p.emitter, _position, _rotation);

    }

}


[System.Serializable]
public class Particle
{
    public string name;
    public GameObject emitter;
}
