using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneChestCollision : MonoBehaviour
{
    [SerializeField] private Sprite _openedChest;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion") && gameObject.tag =="LockedStoneChest")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _openedChest;
            gameObject.transform.tag = "OpenedStoneChest";
            gameObject.GetComponent<ChestDrop>().ChestDroprate();
        }
    }
}
