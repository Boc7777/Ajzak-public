using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSideOfStone : MonoBehaviour
{
    [HideInInspector] public bool IsHoleOnBot;
    [HideInInspector] public bool BombInBot;
    [HideInInspector] public Vector3 BridgeBottomPosition;
    [HideInInspector] public GameObject HoleOnBottom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Holes"))
        {
            IsHoleOnBot = true;
            BridgeBottomPosition = collision.transform.position;
            HoleOnBottom = collision.gameObject;
        }
        if (collision.CompareTag("Bomb"))
        {
            BombInBot = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            BombInBot = false;
        }
    }
}
