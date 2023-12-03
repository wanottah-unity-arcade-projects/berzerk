
using System.Collections;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.13
//

//
// references
// https://answers.unity.com/questions/534314/how-to-delete-instantiated-gameobject.html
//

public class Player1Controller : MonoBehaviour
{
    public static Player1Controller player1;

    // reference to the player's 'Rigidbody' component
    private Rigidbody2D playerRigidbody;

    // reference to the player's 'Animator' component
    public Animator playerAnimator;

    // player death animation
	public GameObject playerDeathAnimation;

    public GameObject playerBullet;

    public Transform weaponHolder;
    public Transform weaponLauncher;

    public SpriteRenderer playerSpriteRenderer;

    // player spawn points
    public Transform[] playerSpawnPoint; 

    private const float PLAYER_MOVE_SPEED = 2.5f;
    private const float PLAYER_FIRE_RATE = 0.4f;

    // player animation states
    private const int ANIMATION_PLAYER_IDLE = 0;
    private const int ANIMATION_MOVE_PLAYER = 1;
    private const int ANIMATION_FIRE_UP = 1;
    private const int ANIMATION_FIRE_UP_RIGHT = 2;
    private const int ANIMATION_FIRE_RIGHT = 3;
    private const int ANIMATION_FIRE_DOWN_RIGHT = 4;
    private const int ANIMATION_FIRE_DOWN = 5;
    private const int ANIMATION_FIRE_DOWN_LEFT = 6;
    private const int ANIMATION_FIRE_LEFT = 7;
    private const int ANIMATION_FIRE_UP_LEFT = 8;

    // weapon launcher positions
    private const float LAUNCHER_OFFSET_X = 0.7f;
    private const float LAUNCHER_OFFSET_Y = 1f;

    private const int NORTH_SECTOR = 2;
    private const int SOUTH_SECTOR = 12;
    private const int EAST_SECTOR = 9;
    private const int WEST_SECTOR = 5;

    // player start position
    //private float player1PositionX;
	//private float player1PositionY;

	private Vector2 player1StartPosition;

    public int playerSector;

    //public int player1Lives;

	private float playerSpeed;
    private int playerFireDirection;

    private float horizontalDirection;
    private float verticalDirection;
    private float controllerDirectionX;
    private float controllerDirectionY;

    // shoot delay
    private float fireRate;
    private float shootDelay;

    private bool playerIsMoving;
    public bool playerIsFacingRight;
    private bool playerIsFiring;
    public bool playerIsDead;

    public bool playerHasLeftRoom;
    public int exit;

    public bool inPlay;




    private void Awake()
	{
        if (player1 != null)
        {
            GameObject.Destroy(player1);
        }

        else
        {
            player1 = this;
        }

        // get reference to player's rigidbody component
        playerRigidbody = GetComponent<Rigidbody2D>();
	}


    // called from game controller
    public void Initialise()
    {
        playerSpeed = PLAYER_MOVE_SPEED;

        playerFireDirection = ANIMATION_PLAYER_IDLE;

        fireRate = PLAYER_FIRE_RATE;

        // reset player 1 start position
        GameController.gameController.playerRespawning = true;

        EnterNewRoom(RoomController.WEST_EXIT);

        //player1Lives = GameController.START_LIVES;
        
        playerIsFacingRight = true;
        playerIsMoving = false;
        playerIsFiring = false;
        playerIsDead = false;
        playerHasLeftRoom = false;

        exit = -1;

        inPlay = false;
    }


    public void EnterNewRoom(int exit)
    {
        // move player
        Vector2 newPlayerPosition;

        if (GameController.gameController.playerRespawning)
        {
            newPlayerPosition.x = playerSpawnPoint[exit].position.x;
            newPlayerPosition.y = playerSpawnPoint[exit].position.y;

            PositionPlayer(new Vector2(newPlayerPosition.x, newPlayerPosition.y));

            GameController.gameController.playerRespawning = false;
        }

        else
        {
            int spawnPoint = GameController.gameController.GetOppositeExit(exit);

            newPlayerPosition.x = playerSpawnPoint[spawnPoint].position.x;
            newPlayerPosition.y = playerSpawnPoint[spawnPoint].position.y;
        }

        switch (exit)
        {
            case RoomController.NORTH_EXIT: playerSector = NORTH_SECTOR; break;
            case RoomController.SOUTH_EXIT: playerSector = SOUTH_SECTOR; break;
            case RoomController.EAST_EXIT: playerSector = EAST_SECTOR; break;
            case RoomController.WEST_EXIT: playerSector = WEST_SECTOR; break;
        }

        PositionPlayer(new Vector2(newPlayerPosition.x, newPlayerPosition.y));
    }


    private void PositionPlayer(Vector2 position)
    {
        player1StartPosition = new Vector2(position.x, position.y);

        transform.position = player1StartPosition;

        StartCoroutine(PlayerStart());
    }


    IEnumerator PlayerStart()
    {
        playerSpriteRenderer.enabled = true;

        playerAnimator.SetBool("playerStart", true);

        yield return new WaitForSeconds(2.5f);

        playerAnimator.SetBool("playerStart", false);

        inPlay = true;
    }


    private void PositionLauncher(float launcherOffsetX, float launcherOffsetY, float launcherRotation)
    {
        // set launcher direction
        weaponLauncher.position = 
            
            new Vector3(
                transform.position.x + launcherOffsetX, 
                transform.position.y + launcherOffsetY, 
                0f);

        weaponLauncher.eulerAngles = new Vector3(0f, 0f, launcherRotation);
    }


    public void CheckPlayerInput()
    {
        if (playerIsDead)
        {
            return;
        }

        PlayerControllerInput();

        PlayerFireInput();

        PlayerMoveAnimation();
    }


    // player 1
    private void PlayerControllerInput()
    {
        playerIsMoving = false;

        if (!playerIsMoving) // || playerIsDead)
        {
            horizontalDirection = 0f;
            verticalDirection = 0f;

            playerRigidbody.velocity = Vector2.zero;
        }

        if (playerHasLeftRoom)
        {
            return;
        }

        controllerDirectionX = Input.GetAxisRaw("Horizontal");
        controllerDirectionY = Input.GetAxisRaw("Vertical");


        // up
        if (controllerDirectionX == 0f && controllerDirectionY > 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = 0f;
            verticalDirection = playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // down
        if (controllerDirectionX == 0f && controllerDirectionY < 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = 0f;
            verticalDirection = -playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // left
        if (controllerDirectionX < 0f && controllerDirectionY == 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = -playerSpeed;
            verticalDirection = 0f;

            playerIsMoving = true;

            MovePlayer();
        }


        // right
        if (controllerDirectionX > 0 && controllerDirectionY == 0f)
        {
            if (playerIsFiring)
            {                
                return;
            }

            horizontalDirection = playerSpeed;
            verticalDirection = 0f;

            playerIsMoving = true;

            MovePlayer();
        }


        // up - left
        if (controllerDirectionY > 0f && controllerDirectionX < 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = -playerSpeed;
            verticalDirection = playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // up - right
        if (controllerDirectionY > 0f && controllerDirectionX > 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = playerSpeed;
            verticalDirection = playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // down - left
        if (controllerDirectionY < 0f && controllerDirectionX < 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = -playerSpeed;
            verticalDirection = -playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // down - right
        if (controllerDirectionY < 0f && controllerDirectionX > 0f)
        {
            if (playerIsFiring)
            {
                return;
            }

            horizontalDirection = playerSpeed;
            verticalDirection = -playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }
    }


    private void PlayerFireInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerIsFiring = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerIsFiring = false;

            playerAnimator.SetInteger("fireDirection", ANIMATION_PLAYER_IDLE);

            return;
        }

        if (controllerDirectionX == 0f && controllerDirectionY == 0f)
        {
            playerAnimator.SetInteger("fireDirection", ANIMATION_PLAYER_IDLE);

            return;
        }

        else
        {
            if (playerIsFiring)
            {
                // up
                if (controllerDirectionX == 0f && controllerDirectionY > 0f)
                {
                    // set launcher direction
                    PositionLauncher(0f, 1f, 90f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f); // 90f);

                    PlayerFireAnimation(ANIMATION_FIRE_UP);
                }


                // down
                else if (controllerDirectionX == 0f && controllerDirectionY < 0f)
                {
                    // set launcher direction
                    PositionLauncher(0f, -1f, 270f);

                    weaponHolder.rotation = Quaternion.Euler(0, 0, 270f);

                    PlayerFireAnimation(ANIMATION_FIRE_DOWN);
                }


                // left
                else if (controllerDirectionX < 0f && controllerDirectionY == 0f)
                {
                    //SetPlayerSpriteDirection();
                    horizontalDirection = -playerSpeed;

                    // set launcher direction
                    PositionLauncher(-0.5f, 0.25f, 180f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f); // 180f);

                    PlayerFireAnimation(ANIMATION_FIRE_LEFT);
                }


                // right
                else if (controllerDirectionX > 0 && controllerDirectionY == 0f)
                {
                    //SetPlayerSpriteDirection();
                    horizontalDirection = playerSpeed;

                    // set launcher direction
                    PositionLauncher(0.5f, 0.25f, 0f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f);

                    PlayerFireAnimation(ANIMATION_FIRE_RIGHT);
                }


                // up - left
                else if (controllerDirectionY > 0f && controllerDirectionX < 0f)
                {
                    // set launcher direction
                    PositionLauncher(-0.7f, 1f, 135f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f);

                    PlayerFireAnimation(ANIMATION_FIRE_UP_LEFT);
                }


                // up - right
                else if (controllerDirectionY > 0f && controllerDirectionX > 0f)
                {
                    // set launcher direction
                    PositionLauncher(0.7f, 1f, 45f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f);

                    PlayerFireAnimation(ANIMATION_FIRE_UP_RIGHT);
                }


                // down - left
                else if (controllerDirectionY < 0f && controllerDirectionX < 0f)
                {
                    // set launcher direction
                    PositionLauncher(-0.7f, -1f, 225f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f);

                    PlayerFireAnimation(ANIMATION_FIRE_DOWN_LEFT);
                }


                // down - right
                else if (controllerDirectionY < 0f && controllerDirectionX > 0f)
                {
                    // set launcher direction
                    PositionLauncher(0.7f, -1f, 315f);

                    weaponHolder.rotation = Quaternion.Euler(0f, 0f, 0f);

                    PlayerFireAnimation(ANIMATION_FIRE_DOWN_RIGHT);
                }
            }
        }
    
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            FirePlayerBullet();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            shootDelay -= Time.deltaTime;

            if (shootDelay <= 0)
            {
                FirePlayerBullet();
            }
        }
    }


    private void PlayerFireAnimation(int fireDirection)
    {
        SetPlayerSpriteDirection();

        playerAnimator.SetInteger("fireDirection", fireDirection);
    }


    private void FirePlayerBullet()
    {
        Instantiate(playerBullet, weaponLauncher.position, weaponLauncher.rotation);

        AudioController.audioController.PlayAudioClip("Player Shoot");

        shootDelay = fireRate;
    }


    private void PlayerMoveAnimation()
    {
        SetPlayerSpriteDirection();

        if (playerIsMoving)
        {
            playerAnimator.SetInteger("playerAnimation", ANIMATION_MOVE_PLAYER);
        }

        else
        {
            playerAnimator.SetInteger("playerAnimation", ANIMATION_PLAYER_IDLE);
        }
    }


    private void SetPlayerSpriteDirection()
    {
        if (horizontalDirection > 0 && !playerIsFacingRight)
        {
            FlipPlayerSprite();
        }

        else if (horizontalDirection < 0 && playerIsFacingRight)
        {
            FlipPlayerSprite();
        }
	}


	public void FlipPlayerSprite()
    {
		playerIsFacingRight = !playerIsFacingRight;

		Vector3 playerTransform = transform.localScale;
 
		playerTransform.x *= -1;

		transform.localScale = playerTransform;
	}


    private void MovePlayer()
    {
        if (playerIsMoving)
        {
            playerRigidbody.velocity = new Vector2(horizontalDirection, verticalDirection);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerIsDead)
        {
            return;
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Wall") || other.CompareTag("Enemy Bullet"))
        {
            playerIsDead = true;

            playerIsFiring = false;

            // stop player moving
            playerRigidbody.velocity = Vector2.zero;

            playerAnimator.SetInteger("playerAnimation", ANIMATION_PLAYER_IDLE);

            // disable the player game object sprite renderer
            playerSpriteRenderer.enabled = false;

            AudioController.audioController.PlayAudioClip("Player Died");

            StartCoroutine(PlayerDied());
        }
    }


	IEnumerator PlayerDied()
    {       
        // create a clone of the player death animation
        GameObject killBill = Instantiate(playerDeathAnimation, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

        Destroy(killBill);

        GameController.gameController.PlayerDied();
	}


} // end of class
