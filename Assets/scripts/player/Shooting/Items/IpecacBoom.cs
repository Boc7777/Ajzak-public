using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpecacBoom : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy(0.5f));
    }
    private IEnumerator Destroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
