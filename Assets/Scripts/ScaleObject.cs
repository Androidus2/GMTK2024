using System;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    private Vector3 originalScale; // Store the original scale of the object
    

    void Start()
    {
        // Store the initial scale of the object when the script starts
        originalScale = transform.localScale;
    }

    public void Scale(Vector3 scaleChange)
    {
        transform.localScale += scaleChange;
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
    }


}
