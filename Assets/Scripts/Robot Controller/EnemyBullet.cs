
using System.Collections;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.14
//

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D enemyBulletRigidbody;

    private float enemyBulletSpeed;

    private Vector2 fireDirection;


    private void Awake()
    {
        enemyBulletRigidbody = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        Initialise();
    }


    private void Initialise()
    {
        enemyBulletSpeed = 7f;

        //enemyBulletRigidbody.velocity = transform.right * enemyBulletSpeed;
    //}


    //private void FireBullet()
    //{
        fireDirection = (EnemyController1.enemyController.playerTransform.position - transform.position).normalized * enemyBulletSpeed;

        enemyBulletRigidbody.velocity = new Vector2(fireDirection.x, fireDirection.y);
    }


    // destroy player bullet when it collides with another object
    private void OnTriggerEnter2D(Collider2D objectCollidedWith)
    {
        Destroy(gameObject);
    }



} // end of class
