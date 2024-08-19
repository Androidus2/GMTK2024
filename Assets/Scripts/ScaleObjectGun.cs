using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObjectGun : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;  // The camera used for aiming
    [SerializeField]
    private float maxDistance = 10f;  // Maximum distance for interacting with objects
    [SerializeField]
    private float scaleAmount = 0.01f;  // Amount to scale the object per key press
    [SerializeField]
    private LayerMask interactableLayer; // Layer mask to define which objects can be scaled
    [SerializeField]
    private Animator anim;  // Animator for handling visual feedback
    [SerializeField]
    private Material shrinkMaterial;
    [SerializeField]
    private Material growMaterial;
    [SerializeField]
    private MeshRenderer righHandEffectRenderer; // Mesh renderer for the right hand

    [SerializeField]
    private float pulsateSpeed = 2f;  // Speed of the pulsating effect
    [SerializeField]
    private float pulsateScale = 0.1f;  // How much to scale during the pulsate effect

    // Reference to the Telekinesis script
    private Telekinesis telekinesis;

    // Dictionary to store each object's original scale and mass
    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, float> originalMasses = new Dictionary<GameObject, float>();

    private float resetDuration = 1f;  // Duration for the gradual reset
    private Vector3 initialHandEffectScale;

    void Start()
    {
        telekinesis = GetComponent<Telekinesis>();
        righHandEffectRenderer.material = growMaterial;
        righHandEffectRenderer.gameObject.SetActive(false);
        initialHandEffectScale = righHandEffectRenderer.transform.localScale; // Store the initial scale
    }

    void Update()
    {
        HandleScaling();
        PulsateEffect();  // Apply pulsating effect
    }

    private void HandleScaling()
    {
        if (telekinesis.GetIsHolding())
        {
            ScaleObject scaleObject = telekinesis.GetScaleObject();
            PerformScale(scaleObject);
        }
        else
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
            {
                ScaleObject scaleObject = hit.transform.GetComponent<ScaleObject>();
                PerformScale(scaleObject);
            }
            else if (anim.GetBool("Grow") || anim.GetBool("Shrink"))
            {
                anim.SetBool("Grow", false);
                anim.SetBool("Shrink", false);
            }
        }
    }

    private void PerformScale(ScaleObject scaleObject)
    {
        if (scaleObject != null)
        {
            GameObject obj = scaleObject.gameObject;

            // Store the original scale and mass if they aren't already stored
            if (!originalScales.ContainsKey(obj))
            {
                originalScales[obj] = obj.transform.localScale;
            }
            if (!originalMasses.ContainsKey(obj))
            {
                originalMasses[obj] = obj.GetComponent<Rigidbody>().mass;
            }

            // Increase/Decrease size
            if (Input.GetKey(KeyCode.Q))
            {
                righHandEffectRenderer.material = shrinkMaterial;
                righHandEffectRenderer.gameObject.SetActive(true);

                scaleObject.Scale(Vector3.one * scaleAmount * Time.deltaTime);
                if (!telekinesis.GetIsHolding())
                {
                    scaleObject.GetComponent<Rigidbody>().mass = originalMasses[obj] * MathF.Pow(scaleObject.transform.localScale.x / originalScales[obj].x, 3);
                }
                anim.SetBool("Grow", true);
                anim.SetBool("Shrink", false);
            }
            else if (Input.GetKey(KeyCode.E) && scaleObject.transform.localScale.x > 0.1f)
            {
                righHandEffectRenderer.material = growMaterial;
                righHandEffectRenderer.gameObject.SetActive(true);

                scaleObject.Scale(Vector3.one * -scaleAmount * Time.deltaTime);
                if (!telekinesis.GetIsHolding())
                {
                    scaleObject.GetComponent<Rigidbody>().mass = originalMasses[obj] * MathF.Pow(scaleObject.transform.localScale.x / originalScales[obj].x, 3);
                }
                anim.SetBool("Shrink", true);
                anim.SetBool("Grow", false);
            }
            else
            {
                righHandEffectRenderer.gameObject.SetActive(false);
                anim.SetBool("Grow", false);
                anim.SetBool("Shrink", false);
            }

            // Reset size with R
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(GradualReset(scaleObject, obj));
                if(!telekinesis.GetIsHolding()) {
                    scaleObject.GetComponent<Rigidbody>().mass = originalMasses[obj];
                }
            }
        }
        else
        {
            anim.SetBool("Grow", false);
            anim.SetBool("Shrink", false);
        }
    }

    private void PulsateEffect()
    {
        if (righHandEffectRenderer.gameObject.activeSelf)
        {
            // Calculate the scale factor using a sine wave
            float scaleFactor = 1 + Mathf.Sin(Time.time * pulsateSpeed) * pulsateScale;
            righHandEffectRenderer.transform.localScale = initialHandEffectScale * scaleFactor;
        }
    }

    private IEnumerator GradualReset(ScaleObject scaleObject, GameObject obj)
    {
        Vector3 currentScale = scaleObject.transform.localScale;
        Vector3 originalScale = originalScales[obj];
        Rigidbody holdRB = scaleObject.GetComponent<Rigidbody>();
        float currentMass = holdRB.mass;
        float originalMass = originalMasses[obj];
        float elapsedTime = 0f;

        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / resetDuration;

            // Interpolate scale and mass
            scaleObject.transform.localScale = Vector3.Lerp(currentScale, originalScale, t);
            // holdRB.mass = Mathf.Lerp(currentMass, originalMass, t);

            yield return null;
        }

        // Ensure final values are set
        scaleObject.transform.localScale = originalScale;
        // holdRB.mass = originalMass;
    }
}