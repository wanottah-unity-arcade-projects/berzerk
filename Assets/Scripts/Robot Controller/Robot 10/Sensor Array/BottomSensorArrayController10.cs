
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.11.26
//

public class BottomSensorArrayController10 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("HIT WALL");
            EnemyController10.enemyController.blockedByWallBottom = true;
        }
    }


} // end of class
