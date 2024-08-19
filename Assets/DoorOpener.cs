using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private List<PressurePlate> pressurePlates;

    [SerializeField]
    private Animator door;

    [SerializeField]
    private bool isDoorOpen = false;
    
    void Update()
    {
        if(AreAllPressurePlatesActivated() && !isDoorOpen)
        {
            isDoorOpen = true;
            door.SetTrigger("Change1");
        }
        else if(!AreAllPressurePlatesActivated() && isDoorOpen)
        {
            isDoorOpen = false;
            door.SetTrigger("Change2");
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

}
