using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import the SceneManagement namespace

public class SnapOnFrame : MonoBehaviour
{
    [SerializeField] private GameObject key; // The object to be snapped
    [SerializeField] private GameObject snapPoint;

    // Static variables to track both keys
    private static bool key1Snapped = false;
    private static bool key2Snapped = false;

    [SerializeField] private bool isKey1; // Use this to identify if the current script is for key 1 or key 2

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == key)
        {
            key.transform.position = snapPoint.transform.position;
            key.transform.rotation = snapPoint.transform.rotation;

            Rigidbody rb = key.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            key.GetComponent<Collider>().enabled = false;
            key.gameObject.layer = 0;

            FindAnyObjectByType<Telekinesis>().DropObject();

            if (isKey1)
            {
                key1Snapped = true;
            }
            else
            {
                key2Snapped = true;
            }

            // Check if both keys are snapped and load the next scene
            if (AreBothKeysSnapped())
            {
                LoadNextScene();
            }
        }
    }

    public static bool AreBothKeysSnapped()
    {
        return key1Snapped && key2Snapped;  // Return true only if both keys are snapped
    }

    private void LoadNextScene()
    {
        // Load the specified scene
        SceneManager.LoadScene("ThirdScene");
    }
}
