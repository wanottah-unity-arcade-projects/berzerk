
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.10
//


public class SecurityDoorController : MonoBehaviour
{
    public static SecurityDoorController doorController;

    public Transform[] securityDoor;


    private void Awake()
    {
        if (doorController != null)
        {
            GameObject.Destroy(doorController);
        }

        else
        {
            doorController = this;
        }
    }


    public void ActivateSecurityDoor(int exit)
    {
        int door = GameController.gameController.GetOppositeExit(exit);

        securityDoor[door].gameObject.SetActive(true);
    }


    public void DeactivateSecurityDoors()
    {
        for (int door = 0; door < securityDoor.Length; door++)
        {
            securityDoor[door].gameObject.SetActive(false);
        }
    }


} // end of class
