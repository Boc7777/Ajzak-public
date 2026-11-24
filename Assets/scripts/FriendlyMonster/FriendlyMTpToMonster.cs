using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FriendlyMTpToMonster : MonoBehaviour
{
    private GameObject _player;
    void Start()
    {
        EventManager.TeleportFamiliar += FriendlyMTeleportIEnumerator;
        _player = GameObject.Find("Player");
    }

    public void FriendlyMTeleportIEnumerator()
    {
        StartCoroutine(FriendlyMTeleportToPlayer());
    }
    private IEnumerator FriendlyMTeleportToPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = _player.transform.position;
    }
}
