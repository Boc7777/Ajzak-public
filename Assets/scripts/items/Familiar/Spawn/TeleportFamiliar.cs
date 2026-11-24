using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFamiliar : MonoBehaviour
{
    private GameObject _player;
    void Start()
    {
        EventManager.TeleportFamiliar +=TeleportIEnumerator ;
        _player = GameObject.Find("Player");
    }

    public void TeleportIEnumerator()
    {
        StartCoroutine(TeleportToPlayer());
    }
    private IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = _player.transform.position;
   }
}
