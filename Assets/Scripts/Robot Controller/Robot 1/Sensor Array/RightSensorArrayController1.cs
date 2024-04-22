
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.11.26
//

public class RightSensorArrayController1 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            EnemyController1.enemyController.blockedByWallRight = true;
        }
    }


} // end of class
