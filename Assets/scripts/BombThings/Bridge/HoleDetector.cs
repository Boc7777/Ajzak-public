using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDetector : MonoBehaviour
{
    [HideInInspector] public bool HoleNearby;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Holes"))
        {
            HoleNearby = true;
        }
    }

    private void Update()
    {
        if (HoleNearby)
        {
            transform.parent.Find("Left").gameObject.SetActive(true);
            transform.parent.Find("Top").gameObject.SetActive(true);
            transform.parent.Find("Right").gameObject.SetActive(true);
            transform.parent.Find("Bottom").gameObject.SetActive(true);
        }
    }
}
