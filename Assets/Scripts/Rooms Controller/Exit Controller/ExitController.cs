
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.13
//

public class ExitController : MonoBehaviour
{
    public static ExitController exitController;

    public Transform[] exitTransform;


    private void Awake()
    {
        if (exitController != null)
        {
            GameObject.Destroy(exitController);
        }

        else
        {
            exitController = this;
        }
    }


    public void ActivateExits()
    {
        for (int exit = 0; exit < exitTransform.Length; exit++)
        {
            exitTransform[exit].gameObject.SetActive(true);
        }
    }


    public void DeactivateExit(int exit)
    {
        int door = GameController.gameController.GetOppositeExit(exit);

        exitTransform[door].gameObject.SetActive(false);
    }


} // end of class
