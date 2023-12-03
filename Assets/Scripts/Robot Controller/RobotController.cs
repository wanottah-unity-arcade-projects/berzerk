
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Berzerk [Stern 1980] v2020.09.03
//
// v2021.12.14
//

public class RobotController : MonoBehaviour
{
    public static RobotController robotController;


    public Transform[] robots;


    private const int NUMBER_OF_SECTORS = 15;
    private const float HORIZONTAL_SECTOR_SIZE = 1.5f;
    private const float VERTICAL_SECTOR_SIZE = 1.35f;

    public const int NUMBER_OF_ROBOTS = 15;
    private const int MINIMUM_NUMBER_OF_ROBOTS = 3;
    private const int MAXIMUM_NUMBER_OF_ROBOTS = 11;

    public const int ROBOT_POINT_VALUE = 50;

    public const float ROBOT_SPEED = 3f;

    public int numberOfRobotsToSpawn;

    private bool[] patrollingSector;

    public bool[] robotDestroyed;


    private void Awake()
    {
        if (robotController != null)
        {
            Destroy(robotController);
        }

        else
        {
            robotController = this;
        }

        patrollingSector = new bool[NUMBER_OF_SECTORS];

        robotDestroyed = new bool[NUMBER_OF_ROBOTS];
    }


    public void SpawnRobots()
    {
        DeactivateAllRobots();

        numberOfRobotsToSpawn = Random.Range(MINIMUM_NUMBER_OF_ROBOTS, 5); // MAXIMUM_NUMBER_OF_ROBOTS + 1);

        int robotCount = numberOfRobotsToSpawn;

        // loop through sectors
        do
        {
            int randomSector = Random.Range(0, NUMBER_OF_SECTORS);

            if (randomSector != Player1Controller.player1.playerSector || !patrollingSector[randomSector])
            {
                //PositionRobot(randomSector);

                // activate the robot
                robots[randomSector].gameObject.SetActive(true);

                // set patrolling sector flag
                patrollingSector[randomSector] = true;

                // get next robot
                robotCount -= 1;
            }
        }
        while (robotCount > 0);
    }


    // deactivate all robots
    public void DeactivateAllRobots()
    {
        for (int robot = 0; robot < NUMBER_OF_SECTORS; robot++)
        {
            robots[robot].gameObject.SetActive(false);

            patrollingSector[robot] = false;

            robotDestroyed[robot] = false;
        }
    }


    // set random position for robot
    private void PositionRobot(int sector)
    {
        // get current robot position
        Vector2 robotPosition = robots[sector].position;

        // get the minimum and maximum positions for the robot in the current sector
        float minimumRobotPositionX = robotPosition.x - HORIZONTAL_SECTOR_SIZE;
        float maximumRobotPositionX = robotPosition.x + HORIZONTAL_SECTOR_SIZE;
        float minimumRobotPositionY = robotPosition.y - VERTICAL_SECTOR_SIZE;
        float maximumRobotPositionY = robotPosition.y + VERTICAL_SECTOR_SIZE;

        // get a random position in the sector
        float spawnPositionX = Random.Range(minimumRobotPositionX, maximumRobotPositionX);
        float spawnPositionY = Random.Range(minimumRobotPositionY, maximumRobotPositionY);

        // position the robot
        robotPosition = new Vector2(spawnPositionX, spawnPositionY);

        robots[sector].position = new Vector2(robotPosition.x, robotPosition.y);
    }


    public void MoveRobots()
    {
        if (Player1Controller.player1.inPlay)
        {
            if (robots[0].gameObject.activeInHierarchy)
            {
                EnemyController1.enemyController.CheckEnemyMovement();

                EnemyController1.enemyController.WeaponCooldown();
            }

            if (robots[1].gameObject.activeInHierarchy)
            {
                EnemyController2.enemyController.CheckEnemyMovement();

                EnemyController2.enemyController.WeaponCooldown();

            }

            if (robots[2].gameObject.activeInHierarchy)
            {
                EnemyController3.enemyController.CheckEnemyMovement();

                EnemyController3.enemyController.WeaponCooldown();

            }

            if (robots[3].gameObject.activeInHierarchy)
            {
                EnemyController4.enemyController.CheckEnemyMovement();

                EnemyController4.enemyController.WeaponCooldown();

            }

            if (robots[4].gameObject.activeInHierarchy)
            {
                EnemyController5.enemyController.CheckEnemyMovement();

                EnemyController5.enemyController.WeaponCooldown();

            }

            if (robots[5].gameObject.activeInHierarchy)
            {
                EnemyController6.enemyController.CheckEnemyMovement();

                EnemyController6.enemyController.WeaponCooldown();

            }

            if (robots[6].gameObject.activeInHierarchy)
            {
                EnemyController7.enemyController.CheckEnemyMovement();

                EnemyController7.enemyController.WeaponCooldown();

            }

            if (robots[7].gameObject.activeInHierarchy)
            {
                EnemyController8.enemyController.CheckEnemyMovement();

                EnemyController8.enemyController.WeaponCooldown();

            }

            if (robots[8].gameObject.activeInHierarchy)
            {
                EnemyController9.enemyController.CheckEnemyMovement();

                EnemyController9.enemyController.WeaponCooldown();

            }

            if (robots[9].gameObject.activeInHierarchy)
            {
                EnemyController10.enemyController.CheckEnemyMovement();

                EnemyController10.enemyController.WeaponCooldown();

            }

            if (robots[10].gameObject.activeInHierarchy)
            {
                EnemyController11.enemyController.CheckEnemyMovement();

                EnemyController11.enemyController.WeaponCooldown();

            }

            if (robots[11].gameObject.activeInHierarchy)
            {
                EnemyController12.enemyController.CheckEnemyMovement();

                EnemyController12.enemyController.WeaponCooldown();

            }

            if (robots[12].gameObject.activeInHierarchy)
            {
                EnemyController13.enemyController.CheckEnemyMovement();

                EnemyController13.enemyController.WeaponCooldown();

            }

            if (robots[13].gameObject.activeInHierarchy)
            {
                EnemyController14.enemyController.CheckEnemyMovement();

                EnemyController14.enemyController.WeaponCooldown();

            }

            if (robots[14].gameObject.activeInHierarchy)
            {
                EnemyController15.enemyController.CheckEnemyMovement();

                EnemyController15.enemyController.WeaponCooldown();

            }
        }       
    }


} // end of class
