using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUpChecker : MonoBehaviour
{
    [HideInInspector]public bool OpenedFromTop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenedFromTop = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenedFromTop = false;
            
        }
    }
}
