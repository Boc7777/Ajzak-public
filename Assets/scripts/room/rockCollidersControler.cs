using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockCollidersControler : MonoBehaviour
{
    [SerializeField] private GameObject leftCollider;
    [SerializeField] private GameObject rightCollider;
    public void leftColliderOn()
    {
        leftCollider.SetActive(true);
    }
    public void rightColliderOn()
    {
        rightCollider.SetActive(true);
    }

}
