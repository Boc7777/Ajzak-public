using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDed : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        EventManager.SwitchAgro += SwitchAgro;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchAgro()
    {
        transform.GetComponent<Monster>().enabled = true;
        transform.GetComponent<MoveAroundDoF>().enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.SwitchAgro -= SwitchAgro;
    }
}
