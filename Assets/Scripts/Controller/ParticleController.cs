using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    void OnParticleSystemStopped()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
