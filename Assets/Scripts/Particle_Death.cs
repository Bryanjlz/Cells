using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Death : MonoBehaviour
{

    public ParticleSystem my_particles;

    // Start is called before the first frame update
    void Start()
    {
        my_particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    public void Action()
    {
        my_particles.Play();
        Destroy(gameObject, 1.0f);
    }
}
