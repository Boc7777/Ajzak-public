using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoor : MonoBehaviour
{
    [SerializeField] string _duration;
    private float _timer = 0;
    [SerializeField] bool _specjalRoomChecker = false;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("central") && _specjalRoomChecker == false)
        {           
            if(_duration == "TOP")
            {
                if(gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.activeSelf == false)
                {
                    gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.SetActive(true);
                }

                if(collision.transform.parent.parent.Find("Grid").Find("door_bottom").gameObject.activeSelf == false)
                {
                    collision.transform.parent.parent.Find("Grid").Find("door_bottom").gameObject.SetActive(true);
                    
                }
            }
            if (_duration == "BOTTOM")
            {
                if (gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.activeSelf == false)
                {
                    gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.SetActive(true);
                }

                if (collision.transform.parent.parent.Find("Grid").Find("door_top").gameObject.activeSelf == false)
                {
                    collision.transform.parent.parent.Find("Grid").Find("door_top").gameObject.SetActive(true);
                   
                }
            }
            if (_duration == "RIGHT")
            {
                if (gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.activeSelf == false)
                {
                    gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.SetActive(true);
                }

                if (collision.transform.parent.parent.Find("Grid").Find("door_left").gameObject.activeSelf == false)
                {
                    collision.transform.parent.parent.Find("Grid").Find("door_left").gameObject.SetActive(true);
                    
                }
            }
            if (_duration == "LEFT")
            {
                if (gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.activeSelf == false)
                {
                    gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.SetActive(true);
                }

                if (collision.transform.parent.parent.Find("Grid").Find("door_right").gameObject.activeSelf == false)
                {
                    collision.transform.parent.parent.Find("Grid").Find("door_right").gameObject.SetActive(true);
                    
                }


            }
        }

        if(collision.CompareTag("central") && _specjalRoomChecker == true)
        {
            if(collision.transform.parent.parent.GetComponent<AddRooms>()._bossRoom == true)
            {
                if (_duration == "TOP")
                {
                    if (gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.activeSelf == false)
                    {
                        gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.SetActive(true);
                    }

                    if (collision.transform.parent.parent.Find("Grid").Find("door_bottom").gameObject.activeSelf == false)
                    {
                        collision.transform.parent.parent.Find("Grid").Find("door_bottom").gameObject.SetActive(true);

                    }
                }
                if (_duration == "BOTTOM")
                {
                    if (gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.activeSelf == false)
                    {
                        gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.SetActive(true);
                    }

                    if (collision.transform.parent.parent.Find("Grid").Find("door_top").gameObject.activeSelf == false)
                    {
                        collision.transform.parent.parent.Find("Grid").Find("door_top").gameObject.SetActive(true);

                    }
                }
                if (_duration == "RIGHT")
                {
                    if (gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.activeSelf == false)
                    {
                        gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.SetActive(true);
                    }

                    if (collision.transform.parent.parent.Find("Grid").Find("door_left").gameObject.activeSelf == false)
                    {
                        collision.transform.parent.parent.Find("Grid").Find("door_left").gameObject.SetActive(true);

                    }
                }
                if (_duration == "LEFT")
                {
                    if (gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.activeSelf == false)
                    {
                        gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.SetActive(true);
                    }

                    if (collision.transform.parent.parent.Find("Grid").Find("door_right").gameObject.activeSelf == false)
                    {
                        collision.transform.parent.parent.Find("Grid").Find("door_right").gameObject.SetActive(true);

                    }


                }
            }
        }


    }
}
