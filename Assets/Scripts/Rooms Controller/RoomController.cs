
using System.Collections;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.14
//

public class RoomController : MonoBehaviour
{
    public static RoomController roomController;

    private Animator roomAnimator;

    public Transform arenaPanel;

    // exits
    private const int NUMBER_OF_EXITS = 4;
    public const int NORTH_EXIT = 0;
    public const int EAST_EXIT = 1;
    public const int SOUTH_EXIT = 2;
    public const int WEST_EXIT = 3;

    private const int ANIMATION_ROOM_IDLE = -1;
    public const int ANIMATION_MOVE_ROOM_DOWN = 0;   // from north exit
    public const int ANIMATION_MOVE_ROOM_LEFT = 1;   // from east exit
    public const int ANIMATION_MOVE_ROOM_UP = 2;     // from south exit
    public const int ANIMATION_MOVE_ROOM_RIGHT = 3;  // from west exit


    private void Awake()
    {
        if (roomController != null)
        {
            Destroy(roomController);
        }

        else
        {
            roomController = this;
        }

        roomAnimator = GetComponent<Animator>();
    }


    public void AnimateRoom(int exit)
    {
        switch (exit)
        {
            case NORTH_EXIT:

                roomAnimator.SetInteger("roomAnimation", ANIMATION_MOVE_ROOM_DOWN);

                break;

            case SOUTH_EXIT:

                roomAnimator.SetInteger("roomAnimation", ANIMATION_MOVE_ROOM_UP);

                break;

            case EAST_EXIT:

                roomAnimator.SetInteger("roomAnimation", ANIMATION_MOVE_ROOM_LEFT);

                break;

            case WEST_EXIT:

                roomAnimator.SetInteger("roomAnimation", ANIMATION_MOVE_ROOM_RIGHT);

                break;
        }

        StartCoroutine(GenerateNewRoom(exit));
    }


    private IEnumerator GenerateNewRoom(int exit)
    {
        yield return new WaitForSeconds(3f);

        RobotController.robotController.DeactivateAllRobots();

        Player1Controller.player1.playerSpriteRenderer.enabled = false;

        yield return new WaitForSeconds(3f);

        roomAnimator.SetInteger("roomAnimation", ANIMATION_ROOM_IDLE);

        transform.position = new Vector3(0f, 0f, 0f);

        arenaPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        SecurityDoorController.doorController.DeactivateSecurityDoors();

        BerzerkMazeGenerator.mazeGenerator.GenerateNewRoom();

        SecurityDoorController.doorController.ActivateSecurityDoor(exit);

        arenaPanel.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        Player1Controller.player1.EnterNewRoom(exit);

        Player1Controller.player1.playerSpriteRenderer.enabled = true;

        Player1Controller.player1.playerHasLeftRoom = false;

        yield return new WaitForSeconds(3f);

        RobotController.robotController.SpawnRobots();

        GameController.gameController.robotsDestroyed = 0;
    }


} // end of class
