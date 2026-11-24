using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private RoomsPrefabs _templates;
    private cetral _cetral;

    private int _rand = 0;

    private GameObject _boss;

    private Transform _parent;

    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _cetral = gameObject.transform.parent.parent.Find("Spawnery").Find("central").gameObject.GetComponent<cetral>();

        _parent = gameObject.transform.parent.parent.Find("Monstery").gameObject.transform;

        _rand = Random.Range(0, _templates.BossList.Count);
        _boss = Instantiate(_templates.BossList[_rand], transform.position, Quaternion.identity, _parent);

        _cetral.SpawnedMonsters.Add(_boss);
    }

   
}
