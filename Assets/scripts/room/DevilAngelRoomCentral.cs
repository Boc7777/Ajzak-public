using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAngelRoomCentral : MonoBehaviour
{
    private RoomsPrefabs _templates;
    
    private void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();

        int _rand = Random.Range(0, _templates.ConfigurationDevilRoom.Count);
        Instantiate(_templates.ConfigurationDevilRoom[_rand], transform.position, Quaternion.identity, transform);

       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(OpenDoors(0.3f));
        }
    }

    IEnumerator OpenDoors(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        
        if (gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_top_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_bottom_open").gameObject.SetActive(true);
        }
        
        if (gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_right_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_left_open").gameObject.SetActive(true);
        }
        
    }
}
