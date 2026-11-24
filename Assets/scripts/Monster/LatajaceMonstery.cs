using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatajaceMonstery : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HitboxMapElement") || collision.gameObject.CompareTag("Rocks"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            //Debug.Log("chuk");
        }
    }

}
