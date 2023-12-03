
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.08
//

public class SouthExitController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player1Controller.player1.playerHasLeftRoom = true;

            RoomController.roomController.AnimateRoom(RoomController.ANIMATION_MOVE_ROOM_UP);
        }
    }


} // end of class
