using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private List<PressurePlate> pressurePlates;

    [SerializeField]
    private int rotationAngle;

    [SerializeField]
    private bool isDoorOpen = false;
    
    void Update()
    {
        if (AreAllPressurePlatesActivated() && !isDoorOpen)
        {
            RotateDoor(rotationAngle);
            isDoorOpen = true;
        } else if (!AreAllPressurePlatesActivated() && isDoorOpen)
        {
            RotateDoor(-rotationAngle);
            isDoorOpen = false;
        }
    }

    private bool AreAllPressurePlatesActivated()
    {
        foreach (var plate in pressurePlates)
        {
            if (plate == null || !plate.GetIsActivated())
            {
                return false;
            }
        }
        return true;
    }

    private void RotateDoor(int rotationAngle)
    {
        transform.Rotate(new Vector3(0, rotationAngle, 0));
    }
}
