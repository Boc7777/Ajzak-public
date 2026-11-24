using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private RoomsPrefabs _templates;
    private cetral _cetral;

    private int _rand=0;

    private GameObject _monster;

    private Transform _parent;
    


    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _cetral = gameObject.transform.parent.parent.Find("Spawnery").Find("central").gameObject.GetComponent<cetral>();

        _parent = gameObject.transform.parent.parent.Find("Monstery").gameObject.transform;

        _rand = Random.Range(0, _templates.Monsters.Count);
        _monster = Instantiate(_templates.Monsters[_rand], transform.position, Quaternion.identity, _parent);

        _cetral.SpawnedMonsters.Add(_monster);

    }
}
