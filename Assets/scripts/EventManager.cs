using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action TeleportFamiliar;
    public static event Action DestroyTears;
    public static event Action SwitchAgro;




    public void DoTeleportFamiliar()
    {
        TeleportFamiliar?.Invoke();
    }
    
    public void FuncDestroyTears()
    {
        DestroyTears?.Invoke();
    }
    public void FuncSwitchAgro()
    {
        SwitchAgro?.Invoke();
    }
    
}
