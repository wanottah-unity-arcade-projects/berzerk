
using System;
using UnityEngine;

//
// Berzerk [Stern 1980] v2020.09.03
//
// v2021.12.12
//

public class Player1ScoreController : MonoBehaviour
{
    public static Player1ScoreController scoreController;

    public SpriteRenderer[] player1Score;

    public SpriteRenderer[] player2Score;

    public SpriteRenderer[] highScore;

    public Sprite[] numberDigits;

    public const int PLAYER_1 = 1;
    public const int PLAYER_2 = 2;
    public const int HIGH_SCORE = 3;


    private void Awake()
    {
        if (scoreController != null)
        {
            Destroy(scoreController);
        }

        else
        {
            scoreController = this;
        }
    }


    public void InitialiseScores()
    {
        for (int scoreDigit = 0; scoreDigit < 5; scoreDigit++)
        {
            player1Score[scoreDigit].sprite = numberDigits[0];

            player1Score[scoreDigit].gameObject.SetActive(false);

            //player2Score[scoreDigit].sprite = numberDigits[0];

            //highScore[scoreDigit].sprite = numberDigits[0];
        }
    }


    public void UpdateScoreDisplay(int score, int display)
    {
        string scoreText = score.ToString();
        
        for (int scoreDigit = 0; scoreDigit < scoreText.Length; scoreDigit++)
        {
            string digitText = scoreText.Substring(scoreDigit, 1);

            int digit = Convert.ToInt32(digitText);

            switch (display)
            {
                case PLAYER_1:

                    UpdatePlayer1(scoreText);

                    break;

                case PLAYER_2:

                    UpdatePlayer2(scoreText, scoreDigit, digit);

                    break;

                case HIGH_SCORE:

                    UpdateHighScore(scoreText, scoreDigit, digit);

                    break;
            }
        }
    }


    private void UpdatePlayer1(string scoreText)
    {
        int[] scoreDigit = new int[5];

        // 0
        player1Score[0].gameObject.SetActive(true);

        // 00
        if (GameController.gameController.player1Score > 9 && GameController.gameController.player1Score < 100)
        {
            player1Score[1].gameObject.SetActive(true);

            scoreDigit[1] = Convert.ToInt32(scoreText);

            scoreDigit[1] /= 10;

            player1Score[1].sprite = numberDigits[scoreDigit[1]];
        }

        // 000
        if (GameController.gameController.player1Score > 99 && GameController.gameController.player1Score < 1000)
        {
            for (int i = 1; i <= 2; i++)
            {
                player1Score[i].gameObject.SetActive(true);

                scoreDigit[i] = Convert.ToInt32(scoreText);
            }

            scoreDigit[1] %= 100;

            scoreDigit[1] /= 10;

            scoreDigit[2] /= 100;

            for (int i = 1; i <= 2; i++)
            {
                player1Score[i].sprite = numberDigits[scoreDigit[i]];
            }

        }

        // 0000
        if (GameController.gameController.player1Score > 999 && GameController.gameController.player1Score < 10000)
        {
            for (int i = 1; i <= 3; i++)
            {
                player1Score[i].gameObject.SetActive(true);

                scoreDigit[i] = Convert.ToInt32(scoreText);
            }

            scoreDigit[1] %= 1000;

            scoreDigit[1] %= 100;

            scoreDigit[1] /= 10;

            scoreDigit[2] %= 1000;

            scoreDigit[2] /= 100;

            scoreDigit[3] /= 1000;
            
            for (int i = 1; i <= 3; i++)
            {
                player1Score[i].sprite = numberDigits[scoreDigit[i]];
            }
        }

        // 00000
        if (GameController.gameController.player1Score > 9999 && GameController.gameController.player1Score < 100000)
        {
            for (int i = 1; i <= 4; i++)
            {
                player1Score[i].gameObject.SetActive(true);

                scoreDigit[i] = Convert.ToInt32(scoreText);
            }

            scoreDigit[1] %= 10000;

            scoreDigit[1] %= 1000;

            scoreDigit[1] %= 100;

            scoreDigit[1] /= 10;

            scoreDigit[2] %= 10000;

            scoreDigit[2] %= 1000;

            scoreDigit[2] /= 100;

            scoreDigit[3] %= 10000;

            scoreDigit[3] /= 1000;

            scoreDigit[4] /= 10000;

            for (int i = 1; i <= 4; i++)
            {
                player1Score[i].sprite = numberDigits[scoreDigit[i]];
            }
        }
    }


    private void UpdatePlayer2(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 00000
            case 5:

                player2Score[scoreDigit].sprite = numberDigits[digit];

                break;

            // 0000
            case 4:

                player2Score[scoreDigit + 1].sprite = numberDigits[digit];

                break;

            // 000
            case 3:

                player2Score[scoreDigit + 2].sprite = numberDigits[digit];

                break;

            // 00
            case 2:

                player2Score[scoreDigit + 3].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                player2Score[scoreDigit + 4].sprite = numberDigits[digit];

                break;
        }
    }


    private void UpdateHighScore(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 00000
            case 5:

                highScore[scoreDigit].sprite = numberDigits[digit];

                break;

            // 0000
            case 4:

                highScore[scoreDigit + 1].sprite = numberDigits[digit];

                break;

            // 000
            case 3:

                highScore[scoreDigit + 2].sprite = numberDigits[digit];

                break;

            // 00
            case 2:

                highScore[scoreDigit + 3].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                highScore[scoreDigit + 4].sprite = numberDigits[digit];

                break;
        }
    }


} // end of class
