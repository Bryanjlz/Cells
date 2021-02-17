using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public void death()
    {
        Instantiate(transform.GetChild(0).GetComponent<ParticleSystem>(), transform.position, Quaternion.identity).GetComponent<Particle_Death>().Action();
    }
}
