using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private float requiredMass = 1f;

    [SerializeField]
    private float maximumMass = 2f;

    [SerializeField]
    private bool isActivated = false;

    [SerializeField]
    private GameObject panel;
    private TextMeshProUGUI text;

    private void Start() {
        panel = GameObject.FindGameObjectsWithTag("pressure")[0];
        text = panel.GetComponentInChildren<TextMeshProUGUI>();
        
    }

    
    
    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable") && !isActivated)
        {   
            float objectMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            if(objectMass >= requiredMass && objectMass <= maximumMass)
            {
                
                isActivated = true;
                StartCoroutine(PressureFeedbackActive());
                
            }else if(objectMass > maximumMass)
            {
                
                StartCoroutine(PressureFeedbackTooHeavy());
            } else if (objectMass < requiredMass)
            {
                
                StartCoroutine(PressureFeedbackTooLight());
            }
        }
    }

    IEnumerator PressureFeedbackActive()
    {
        
        text.text = "Pressure plate activated!";
        text.color = Color.green;
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

    IEnumerator PressureFeedbackTooHeavy()
    {
        text.text = "Object is too heavy!";
        text.color = Color.red;
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

    IEnumerator PressureFeedbackTooLight()
    {
        text.text = "Object is too light!";
        text.color = Color.red;
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }

    public bool GetIsActivated()
    {
        return isActivated;
    }

}
