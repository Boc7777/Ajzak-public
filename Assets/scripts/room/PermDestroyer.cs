using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermDestroyer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("destroyer"))
        {
            if (other.gameObject.GetComponent<DestroyerBossRoom>() != null)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("central"))
        {
            Destroy(gameObject);
        }
        
    }
}
