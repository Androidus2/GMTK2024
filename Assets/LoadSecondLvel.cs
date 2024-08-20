using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSecondLvel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SecondScene");
        }
    }
}
