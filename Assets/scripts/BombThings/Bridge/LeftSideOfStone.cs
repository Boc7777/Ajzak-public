using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideOfStone : MonoBehaviour
{
    [HideInInspector] public bool IsHoleOnLeft;
    [HideInInspector] public bool BombInLeft;
    [HideInInspector] public Vector3 LeftBridgePosition;
    [HideInInspector] public GameObject HoleOnLeft;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Holes"))
        {
            IsHoleOnLeft = true;
            LeftBridgePosition = collision.transform.position;
            HoleOnLeft = collision.gameObject;
        }
        if (collision.CompareTag("Bomb"))
        {
            BombInLeft = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            BombInLeft = false;
        }
    }


    
}
