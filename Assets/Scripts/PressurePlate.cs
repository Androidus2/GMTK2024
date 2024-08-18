using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private float requiredMass = 1f;

    [SerializeField]
    private float maximumMass = 2f;

    [SerializeField]
    private bool isActivated;
    
    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable"))
        {   
            float objectMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            if(objectMass >= requiredMass && objectMass <= maximumMass)
            {
                Debug.Log("Pressure plate activated!");
                isActivated = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable") && isActivated)
        {
            isActivated = false;
            Debug.Log("Pressure plate deactivated!");
        }
    }
}
