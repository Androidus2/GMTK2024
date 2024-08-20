using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTriggerEvent : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> objects;

    [SerializeField]
    private bool isTriggered = false;


    void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && !isTriggered)
        {
            isTriggered = true;
            Debug.Log("OnTriggerEnter");
            foreach (var obj in objects)
            {
                obj.useGravity = true;
            }
        }
        Debug.Log("OnTriggerEnter");
    }
}
