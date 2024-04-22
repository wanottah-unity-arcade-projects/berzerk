
using UnityEngine;

//
// Berzerk [Stern 1980] v2020.09.03
//
// v2021.12.13
//

public class CreditsController : MonoBehaviour
{
    public static CreditsController creditsController;

    public SpriteRenderer[] gameCreditDigit;


    private void Awake()
    {
        if (creditsController != null)
        {
            GameObject.Destroy(creditsController);
        }

        else
        {
            creditsController = this;
        }
    }


    public void UpdateGameCredits()
    {
        gameCreditDigit[0].gameObject.SetActive(false);

        gameCreditDigit[1].sprite = GameController.gameController.number[GameController.gameController.gameCredits % 10];

        if (GameController.gameController.gameCredits > 9)
        {
            gameCreditDigit[0].gameObject.SetActive(true);

            gameCreditDigit[0].sprite = GameController.gameController.number[GameController.gameController.gameCredits / 10];
        }
    }


} // end of class
