
using UnityEngine;
using System.Collections;

//
// Berzerk v2020.09.03
//
// v2021.12.14
//

//
// references
// https://www.youtube.com/watch?v=4Wh22ynlLyk
// https://gamedev.stackexchange.com/questions/191291/make-enemy-move-diagonally-in-2d
//

public class EnemyController2 : MonoBehaviour
{
    public static EnemyController2 enemyController;

    // reference to player character
    public Transform playerTransform;

    private Rigidbody2D enemyRigidbody;

    private Animator enemyAnimator;

    // player death animation
    public GameObject enemyDeathAnimation;

    public GameObject enemyBullet;

    public Transform weaponHolder;
    public Transform weaponLauncher;

    public SpriteRenderer enemySpriteRenderer;

    private float enemySpeed;

    private float shootDelay;


    private Vector2 direction;

    //private bool isMove;

    public bool blockedByWallTop;
    public bool blockedByWallRight;
    public bool blockedByWallBottom;
    public bool blockedByWallLeft;

    private bool robotIsFacingRight;
    private bool robotIsMoving;


    private void Awake()
    {
        if (enemyController != null)
        {
            GameObject.Destroy(enemyController);
        }

        else
        {
            enemyController = this;
        }
    }


    private void Start()
    {
        Initialise();
    }


    private void Initialise()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();

        enemyAnimator = GetComponent<Animator>();

        //direction = Random.Range(1, 9);
        enemySpeed = RobotController.ROBOT_SPEED;

        //isMove = true;

        robotIsFacingRight = false;
        robotIsMoving = false;
        //playerIsFiring = false;

        blockedByWallTop = false;
        blockedByWallRight = false;
        blockedByWallBottom = false;
        blockedByWallLeft = false;
    }


    public void CheckEnemyMovement()
    {
        if (RobotController.robotController.robotDestroyed[1])
        {
            return;
        }

        if (blockedByWallTop)
        {
            blockedByWallTop = false;

            return;
        }

        if (blockedByWallRight)
        {
            blockedByWallRight = false;

            return;
        }

        if (blockedByWallBottom)
        {
            blockedByWallBottom = false;

            return;
        }

        if (blockedByWallLeft)
        {
            blockedByWallLeft = false;

            return;
        }

        //StartCoroutine(MoveEnemy(direction));
        MoveRobot();
    }


    private void RobotMoveAnimation()
    {
        SetRobotSpriteDirection();

        if (robotIsMoving)
        {
            //robotAnimator.SetInteger("robotAnimation", ANIMATION_MOVE_ROBOT;
        }

        else
        {
            //robotAnimator.SetInteger("robotAnimation", ANIMATION_ROBOT_IDLE);
        }
    }


    private void SetRobotSpriteDirection()
    {
        /*if (horizontalDirection > 0 && !robotIsFacingRight)
        {
            FlipRobotSprite();
        }

        else if (horizontalDirection < 0 && robotIsFacingRight)
        {
            FlipRobotSprite();
        }*/
    }


    public void FlipRobotSprite()
    {
        robotIsFacingRight = !robotIsFacingRight;

        Vector3 robotTransform = transform.localScale;

        robotTransform.x *= -1;

        transform.localScale = robotTransform;
    }


    //IEnumerator MoveEnemy(Vector2 direction) // GetPlayerPosition()
    private void MoveRobot()
    {
        //if (robotIsMoving)
        if (RobotController.robotController.robotDestroyed[1])
        {
            return;
        }


        //if (isMove)
        //{
        // get position of player
        Vector3 playerPosition = playerTransform.position - transform.position;

        playerPosition.Normalize();

        playerPosition.x = Mathf.Round(playerPosition.x);
        playerPosition.y = Mathf.Round(playerPosition.y);

        direction = playerPosition;

        enemyRigidbody.MovePosition((Vector2)transform.position + (direction * enemySpeed * Time.deltaTime));

        /*yield return new WaitForSeconds(3);

        isMove = false;

        // set animation

        yield return new WaitForSeconds(5);

        isMove = true;*/



        //}
        //}
    }


    /*private void MoveEnemy(Vector2 direction)
    {
        //enemyRigidbody.MovePosition((Vector2)transform.position + (direction * enemySpeed * Time.deltaTime));


    }*/


    public void WeaponCooldown()
    {
        shootDelay -= Time.deltaTime;

        if (shootDelay <= 0)
        {
            FireEnemyBullet();
        }
    }


    private void FireEnemyBullet()
    {
        Instantiate(enemyBullet, weaponLauncher.position, weaponLauncher.rotation);

        AudioController.audioController.PlayAudioClip("Enemy Shoot");

        shootDelay = Random.Range(3f, 8f);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (RobotController.robotController.robotDestroyed[1])
        {
            return;
        }

        if (other.CompareTag("Player Bullet"))
        {
            RobotController.robotController.robotDestroyed[1] = true;

            //playerIsFiring = false;

            enemyRigidbody.velocity = Vector2.zero;

            GameController.gameController.UpdatePlayerScore(RobotController.ROBOT_POINT_VALUE);

            AudioController.audioController.PlayAudioClip("Enemy Died");

            StartCoroutine(RobotDestroyed());
        }
    }

    IEnumerator RobotDestroyed()
    {
        // create a clone of the player death animation
        GameObject killBill = Instantiate(enemyDeathAnimation, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.6f);

        Destroy(killBill);

        Debug.Log("ROBOT DESTROYED");

        gameObject.SetActive(false);
    }


} // end of class
