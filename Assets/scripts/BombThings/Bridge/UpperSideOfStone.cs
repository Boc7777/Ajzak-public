using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSideOfStone : MonoBehaviour
{
    [HideInInspector] public bool IsHoleOnTop;
    [HideInInspector] public bool BombInTop;
    [HideInInspector] public Vector3 BridgeUpPosition;
    [HideInInspector] public GameObject HoleOnTop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Holes"))
        {
            IsHoleOnTop = true;
            BridgeUpPosition = collision.transform.position;
            HoleOnTop = collision.gameObject;
        }
        if (collision.CompareTag("Bomb"))
        {
            BombInTop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            BombInTop = false;
        }
    }
}
