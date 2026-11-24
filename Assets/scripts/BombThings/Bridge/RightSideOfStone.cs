using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideOfStone : MonoBehaviour
{
    [HideInInspector] public bool IsHoleOnRight;
    [HideInInspector] public bool BombInRight;
    [HideInInspector] public Vector3 BridgeRightPosition;
    [HideInInspector] public GameObject HoleOnRight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Holes"))
        {
            IsHoleOnRight = true;
            BridgeRightPosition = collision.transform.position;
            HoleOnRight = collision.gameObject;
        }
        if (collision.CompareTag("Bomb"))
        {
            BombInRight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            BombInRight = false;
        }
    }

    
}
