using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    ParticleSystem ps;
    ParticleSystem.MainModule main;

    void Start()
    {
        ps = transform.GetComponent<ParticleSystem>();
        main = ps.main;
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(1f);
        main.simulationSpeed = 1f;
    }
}
