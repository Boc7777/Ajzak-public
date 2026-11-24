using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [SerializeField]private GameObject _bomb;
    private Pickups _pickups;
    
    void Start()
    {
        _pickups = GameObject.Find("HitBox").GetComponent<Pickups>();
        for (int i = 0; i < _bomb.transform.childCount; i++)
        {
            _bomb.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

   
    private void Update()
    {
        if (Input.GetKeyDown("e") && _pickups.BombsAmount >=1)
        {
            _pickups.BombsAmount--;
            Instantiate(_bomb , gameObject.transform.position - new Vector3(0,0.3f,0) , Quaternion.identity);
            
        }
       
    }
    

}
