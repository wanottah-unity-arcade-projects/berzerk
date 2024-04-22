
using UnityEngine;
using UnityEngine.UI;

//
// Atari Video Game Console Controller v2019.12.17
//
// v2021.12.10
//

public class CoinSlotController : MonoBehaviour
{
    public static CoinSlotController coinController;

    [SerializeField] private Text coinsInsertedText;

    [SerializeField] private Text insertCoinsText;
    [SerializeField] private Text pressGameSelectText;
    [SerializeField] private Text pressStartText;

    // game credits
    private const int INSERT_COINS = 0;
    public const int ONE_PLAYER_COINS = 1;
    private const int MAXIMUM_COINS = 1;

    // game credits
    public int gameCredits;

    public bool canPlay;


    private void Awake()
    {
        coinController = this;
    }


    private void Start()
    {
        InitialiseCoinSlot();
    }


    public void InitialiseCoinSlot()
    {
        gameCredits = INSERT_COINS;

        UpdateGameCreditsText();
    }


    public void CoinSlotControl()
    {
        gameCredits += 1;

        if (gameCredits > MAXIMUM_COINS)
        {
            gameCredits = MAXIMUM_COINS;
        }

        AudioController.audioController.PlayAudioClip("Coin Inserted");

        AudioController.audioController.PlayAudioClip("1UP Credit");

        UpdateGameCreditsText();

        //insertCoinsText.gameObject.SetActive(false);

        canPlay = true;
    }


    public void UpdateGameCreditsText()
    {
        coinsInsertedText.text = gameCredits.ToString("00");
    }


} // end of class
