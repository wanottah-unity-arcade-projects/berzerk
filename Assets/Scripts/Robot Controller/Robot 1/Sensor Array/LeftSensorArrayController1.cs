
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.11.26
//

public class LeftSensorArrayController1 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            EnemyController1.enemyController.blockedByWallLeft = true;
        }
    }


} // end of class
