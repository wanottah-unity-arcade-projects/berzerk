
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.14
//

public class BonusController : MonoBehaviour
{
    public static BonusController bonusController;

    public Transform bonusScoreTextTransform;

    public SpriteRenderer[] player1BonusScore;

    public Sprite[] numberDigits;


    private const int BONUS_SCORE = 10;

    private const int NUMBER_OF_BONUS_SCORE_DIGITS = 3;

    private int bonusScore;


    private void Awake()
    {
        if (bonusController != null)
        {
            Destroy(bonusController);
        }

        else
        {
            bonusController = this;
        }
    }


    public void InitialiseBonusScore()
    {
        for (int bonusScoreDigit = 0; bonusScoreDigit < NUMBER_OF_BONUS_SCORE_DIGITS; bonusScoreDigit++)
        {
            player1BonusScore[bonusScoreDigit].sprite = numberDigits[0];

            player1BonusScore[bonusScoreDigit].gameObject.SetActive(false);
        }
    }


    public void AddBonusPoints()
    {
        bonusScore = GameController.gameController.robotsDestroyed * BONUS_SCORE;

        GameController.gameController.player1Score += bonusScore;

        UpdateBonusScoreDisplay(bonusScore, GameController.PLAYER_ONE);
    }


    public void UpdateBonusScoreDisplay(int bonusScoreInt, int display)
    {
        string bonusScoreString = bonusScoreInt.ToString();
        
        for (int scoreDigit = 0; scoreDigit < bonusScoreString.Length; scoreDigit++)
        {
            //string digitText = bonusScoreString.Substring(scoreDigit, 1);

            //int digit = Convert.ToInt32(digitText);

            switch (display)
            {
                case GameController.PLAYER_ONE:

                    StartCoroutine(BonusScoreDisplayDelay(bonusScoreString));

                    break;

                /*case PLAYER_2:

                    UpdatePlayer2(bonusScoreText, scoreDigit, digit);

                    break;*/
            }
        }
    }


    IEnumerator BonusScoreDisplayDelay(string bonusScoreString)
    {
        UpdatePlayer1(bonusScoreString);

        yield return new WaitForSeconds(3f);

        bonusScoreTextTransform.gameObject.SetActive(false);

        for (int bonusScoreDigit = 0; bonusScoreDigit < NUMBER_OF_BONUS_SCORE_DIGITS; bonusScoreDigit++)
        {
            player1BonusScore[bonusScoreDigit].gameObject.SetActive(false);
        }
    }


    private void UpdatePlayer1(string scoreText)
    {
        bonusScoreTextTransform.gameObject.SetActive(true);

        int[] bonusScoreDigit = new int[NUMBER_OF_BONUS_SCORE_DIGITS];

        // 0
        player1BonusScore[0].gameObject.SetActive(true);

        // 00
        if (bonusScore > 9 && bonusScore < 100)
        {
            player1BonusScore[1].gameObject.SetActive(true);

            bonusScoreDigit[1] = Convert.ToInt32(scoreText);

            bonusScoreDigit[1] /= 10;

            player1BonusScore[1].sprite = numberDigits[bonusScoreDigit[1]];
        }

        // 000
        if (bonusScore > 99 && bonusScore < 1000)
        {
            for (int i = 1; i <= 2; i++)
            {
                player1BonusScore[i].gameObject.SetActive(true);

                bonusScoreDigit[i] = Convert.ToInt32(scoreText);
            }

            bonusScoreDigit[1] %= 100;

            bonusScoreDigit[1] /= 10;

            bonusScoreDigit[2] /= 100;

            for (int i = 1; i <= 2; i++)
            {
                player1BonusScore[i].sprite = numberDigits[bonusScoreDigit[i]];
            }

        }
    }


} // end of class
