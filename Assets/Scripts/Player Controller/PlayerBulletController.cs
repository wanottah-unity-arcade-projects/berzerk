
using System.Collections;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.14
//

public class PlayerBulletController : MonoBehaviour
{
    // reference to player's missile rigidbody components
    private Rigidbody2D playerBulletRigidbody;

    // enemy death animation
    public GameObject enemyDeathAnimation;


    // speed of bullet
    private float playerBulletSpeed;

	private Vector3 playerBulletDirection;


    private void Awake()
    {
        playerBulletRigidbody = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        Initialise();
    }


    private void Initialise()
    {
        playerBulletSpeed = 10f;

        playerBulletRigidbody.velocity = transform.right * playerBulletSpeed;
    }


    // destroy player bullet when it collides with another object
    private void OnTriggerEnter2D(Collider2D objectCollidedWith)
    {
        Destroy(gameObject);
    }


} // end of class
