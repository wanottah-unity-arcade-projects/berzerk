
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// Berzerk Maze Generator v2020.09.02
//
// v2021.11.06
//


public class BerzerkMazeGenerator : MonoBehaviour
{
    public static BerzerkMazeGenerator mazeGenerator;


    private int startRoomInt;

    public int currentRoomNumberInt;


    private string[] pillar;

    private int[] wallDirection;

    public List<Transform> wall;


    private string hexSeed;

    private const int NUMBER_OF_PILLARS = 8;

    private const int NUMBER_OF_WALLS = 4;


    public bool firstRoom;



    private void Awake()
    {
        mazeGenerator = this;
    }


    private void Start()
    {
        Initialise();

        BuildRoom();
    }


    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!firstRoom)
            {
                NewRoom();
            }

            currentRoomNumberInt += 1;

            BuildRoom();
        }
    }*/


    private void Initialise()
    {
        // set hex value
        hexSeed = "3153";

        pillar = new string[NUMBER_OF_PILLARS];

        wallDirection = new int[NUMBER_OF_PILLARS];


        // set starting room number
        startRoomInt = SetStartRoom();

        currentRoomNumberInt = startRoomInt;

        firstRoom = true;
    }


    public void GenerateNewRoom()
    {
        if (!firstRoom)
        {
            NewRoom();
        }

        currentRoomNumberInt += 1;

        BuildRoom();
    }


    private void NewRoom()
    {
        GetPillars(currentRoomNumberInt);

        SetWalls(false);
    }


    private void BuildRoom()
    {
        GetPillars(currentRoomNumberInt);

        SetWalls(true);

        firstRoom = false;
    }


    private int SetStartRoom()
    {
        int startingRoomInt;

        startingRoomInt = 0;

        return startingRoomInt;
    }


    private void GetPillars(int currentRoomNumberInt)
    {
        int pillarIntAddress;

        string pillarHexAddress;


        // first pillar
        pillarIntAddress = currentRoomNumberInt;

        pillarHexAddress = GetPillar(true, pillarIntAddress);

        pillar[0] = pillarHexAddress.Substring(pillarHexAddress.Length - 4, 4);

        //Debug.Log("pillar[0]: " + pillar[0]);


        // pillars 2 to 8
        for (int i = 1; i < NUMBER_OF_PILLARS; i++)
        {
            pillarIntAddress = ConvertHexToInt(pillar[i - 1]);

            pillarHexAddress = GetPillar(false, pillarIntAddress);

            pillar[i] = pillarHexAddress.Substring(pillarHexAddress.Length - 4, 4);


            //Debug.Log("pillar[" + i + "]: " + pillar[i]);
        }

    }


    private string GetPillar(bool firstPillar, int pillarIntAddress)
    {
        string pillarHexAddress;


        // convert pillar integer address to hex
        pillarHexAddress = ConvertIntToHex(pillarIntAddress);

        // display result
        //Debug.Log("pillar hex: " + pillarHex);

        // first and second pass
        for (int i = 1; i <= 2; i++)
        {
            pillarHexAddress = GetPillarHexAddress(pillarHexAddress);
        }


        if (firstPillar)
        {
            // third pass
            pillarHexAddress = GetPillarHexAddress(pillarHexAddress);
        }


        return pillarHexAddress;
    }


    private string GetPillarHexAddress(string pillarHexAddress)
    {
        // multiply be seven
        pillarHexAddress = MultiplyBySeven(pillarHexAddress);

        // add hex seed
        pillarHexAddress = AddHexSeed(pillarHexAddress);

        return pillarHexAddress;
    }


    private void SetWalls(bool wallState)
    {
        string convertedBinaryString;

        string paddedBinaryString;

        string highBit;
        string lowBit;

        string directionBit;

        int wallDirectionBit;


        int wallTransformBit = 0;


        for (int i = 0; i < NUMBER_OF_PILLARS; i++)
        {
            // get binary string from pillar hex address
            convertedBinaryString = ConvertHexToBinary(pillar[i]);

            // pad out string as required
            paddedBinaryString = convertedBinaryString.PadLeft(16, '0');

            // get high 8 bits of address
            highBit = paddedBinaryString.Substring(0, 8);

            // get low 8 bits of address
            lowBit = paddedBinaryString.Substring(8, 8);

            // get bits 1 and 0 from the high bit address
            directionBit = highBit.Substring(highBit.Length - 2, 2);

            // convert to integer
            wallDirectionBit = ConvertBinaryToInt(directionBit);

            wallDirection[i] = wallDirectionBit;

            //Debug.Log(paddedBinaryString);
            //Debug.Log(highBit + ", " + lowBit + ", " + directionBit + ", " + wallDirection[i]);

            wall[wallDirection[i] + wallTransformBit].gameObject.SetActive(wallState);

            wallTransformBit += NUMBER_OF_WALLS;
        }
    }


    private string MultiplyBySeven(string currentPillarHexAddress)
    {
        int nextPillarIntAddress;

        string nextPillarHexAddress;


        // convert pillar address to integer
        nextPillarIntAddress = ConvertHexToInt(currentPillarHexAddress);

        // multiply address by 7
        nextPillarIntAddress = nextPillarIntAddress * 7;

        // convert address to hex
        nextPillarHexAddress = ConvertIntToHex(nextPillarIntAddress);

        // display result
        //Debug.Log("pillar hex: " + multipliedHex);

        return nextPillarHexAddress;
    }


    private string AddHexSeed(string pillarHexAddress)
    {
        int intSeed;

        int pillarIntAddress;

        string nextPillarHexAddress;


        // convert hex seed to integer
        intSeed = ConvertHexToInt(hexSeed);

        // convert pillar address to integer
        pillarIntAddress = ConvertHexToInt(pillarHexAddress);

        // add the seed to the address
        pillarIntAddress = pillarIntAddress + intSeed;

        // convert address to hex
        nextPillarHexAddress = ConvertIntToHex(pillarIntAddress);

        // display result
        //Debug.Log("pillar hex: " + addedValueHex);

        return nextPillarHexAddress;
    }


    private string ConvertIntToHex(int pillarIntAddress)
    {
        return pillarIntAddress.ToString("X4");
    }


    private int ConvertHexToInt(string pillarHexAddress)
    {
        return Convert.ToInt32(pillarHexAddress, 16);
    }


    private string ConvertHexToBinary(string hexAddress)
    {
        string binaryString;

        binaryString = Convert.ToString(Convert.ToInt32(hexAddress, 16), 2);

        return binaryString;
    }


    private int ConvertBinaryToInt(string binaryString)
    {
        int wallDirection;

        wallDirection = Convert.ToInt32(binaryString, 2);

        return wallDirection;
    }


} // end of class

