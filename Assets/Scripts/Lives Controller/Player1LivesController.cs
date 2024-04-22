
using UnityEngine;

//
// Berzerk v2020.09.03
//
// v2021.12.14
//

public class Player1LivesController : MonoBehaviour
{
    public static Player1LivesController livesController;

    public Transform[] lives;

    private const int EXTRA_LIFE = 0;
    private const int MAXIMUM_EXTRA_LIVES = 5;



    private void Awake()
    {
        if (livesController != null)
        {
            GameObject.Destroy(livesController);
        }

        else
        {
            livesController = this;
        }
    }


    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < livesRemaining - 1; i++)
        {
            lives[i].gameObject.SetActive(true);
        }
    }


    public void ExtraLife()
    {
        if (GameController.gameController.player1Score % 5000 == EXTRA_LIFE)
        {
            if (GameController.gameController.player1Lives < MAXIMUM_EXTRA_LIVES)
            {
                GameController.gameController.player1Lives += 1;

                UpdateLives(GameController.gameController.player1Lives);

                AudioController.audioController.PlayAudioClip("Extra Life");
            }
        }
    }


} // end of class
