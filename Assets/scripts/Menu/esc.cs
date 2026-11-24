using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esc : MonoBehaviour
{
    private bool escbool = false;
    public GameObject escButtonsManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escbool)
            {
                escbool = false;
                Time.timeScale = 1;
                escButtonsManager.SetActive(false);
            }
            else
            {
                escbool = true;
                Time.timeScale = 0;
                escButtonsManager.SetActive(true);
            }
        }
    }
}
