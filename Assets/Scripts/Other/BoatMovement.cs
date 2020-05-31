using System.Collections;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    float f, h;

    void Start()
    {
        f = Random.Range(-3f, 3f);
        StartCoroutine(RandomNumber());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * f);
    }

    IEnumerator RandomNumber()
    {
        yield return new WaitForSeconds(3f);
        f = Random.Range(-5f, 5f);
        StartCoroutine(RandomNumber());
    }

}
