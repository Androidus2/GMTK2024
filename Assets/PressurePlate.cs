using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private float requiredMass = 1f;

    [SerializeField]
    private float maximumMass = 2f;
    
    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable"))
        {   
            float objectMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            Debug.Log(objectMass);
            if(objectMass >= requiredMass && objectMass <= maximumMass)
            {
                Debug.Log("Pressure plate activated!");
            }
        }
    }
}
