using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] 
    private Animator wallAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) { 
            wallAnim.SetTrigger("Fall");
        }
    }

}
