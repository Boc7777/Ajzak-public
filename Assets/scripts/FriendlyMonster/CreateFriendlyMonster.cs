using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFriendlyMonster : MonoBehaviour
{
    [SerializeField] private GameObject _guppyFly;
    [SerializeField] private Transform _pokeDex; void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBlueFly()
    {
        Transform playerCord = GameObject.Find("Player").transform;
        int flyCount = Random.Range(1, 3);
        List<Vector3> spawnFly = new List<Vector3> { playerCord.position + new Vector3(0, 1f), playerCord.position + new Vector3(0, -1f, 0), playerCord.position + new Vector3(-1f, 0, 0), playerCord.position + new Vector3(1f, 0, 0) };
        for (int i = 0; i < flyCount * 2; i++)
        {
            Instantiate(_guppyFly, spawnFly[i], Quaternion.identity, _pokeDex);
        }
    }

    public void CreateGuppysFly()
    {
        Transform playerCord = GameObject.Find("Player").transform;
        int flyCount = Random.Range(0, 2);
        List<Vector3> spawnFly = new List<Vector3> { playerCord.position + new Vector3(0, 1f), playerCord.position + new Vector3(0, -1f, 0), playerCord.position + new Vector3(-1f, 0, 0), playerCord.position + new Vector3(1f, 0, 0) };
        for (int i = 0; i < flyCount; i++)
        {
            Instantiate(_guppyFly, spawnFly[i], Quaternion.identity, _pokeDex);
        }
    }
}
