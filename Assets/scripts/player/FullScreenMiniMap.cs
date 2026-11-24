using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenMiniMap : MonoBehaviour
{
    private Camera _miniMapCamera;

    private void Start()
    {

        _miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").gameObject.GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetKey("tab"))
        {
            _miniMapCamera.fieldOfView = 162f;
        }   
        else
        {
            _miniMapCamera.fieldOfView = 140f;
        }
    }
   
}
