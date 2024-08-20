using System;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    private Vector3 originalScale; // Store the original scale of the object

    private AudioSource hitSound;
    

    void Start()
    {
        // Store the initial scale of the object when the script starts
        originalScale = transform.localScale;

        // Get the AudioSource component attached to the object
        hitSound = GetComponent<AudioSource>();
    }

    public void Scale(Vector3 scaleChange)
    {
        transform.localScale += scaleChange;
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Play the hit sound when the object collides with another object
        if(hitSound != null)
            hitSound.Play();
        else
            Debug.LogWarning("No hit sound assigned to " + gameObject.name);
    }


}
