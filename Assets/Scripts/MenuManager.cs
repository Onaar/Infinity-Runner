using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text highScoreValue;
    public Text coinsValue;
    public Text soundBtnText;
    int hs = 0;
    int coins = 0;
    public GameObject mainMenuPanel;
    public GameObject storePanel;

    public Powerup magnet;
    public Powerup immortality;

    public Text magnetLevelText;
    public Text magnetButtonText;

    public Text immortalityLevelText;
    public Text immortalityButtonText;


    private void Start()
    {
        //if (PlayerPrefs.HasKey("HighScoreValue"))
        //{
        //    hs = PlayerPrefs.GetInt("HighScoreValue");
        //}
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.COINS))
        {
            coins = PlayerPrefs.GetInt(PlayerPrefsKeys.COINS);
        }

        mainMenuPanel.SetActive(true);
        storePanel.SetActive(false);

        UpdateUI();
    }
    public void UpdateUI()
    {
        highScoreValue.text = hs.ToString();
        coinsValue.text = coins.ToString();
        if (SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "TURN ON SOUND";
        }
        else
        {
            soundBtnText.text = "TURN OFF SOUND";
        }
        immortalityLevelText.text = immortality.ToString();
        immortalityButtonText.text = immortality.UpgradeCostString();

        magnetLevelText.text = magnet.ToString();
        magnetButtonText.text = magnet.UpgradeCostString();

    }
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SoundButton()
    {
        SoundManager.instance.ToggleMuted();
        UpdateUI();
    }
    public void OpenStore()
    {
        mainMenuPanel.SetActive(false);
        storePanel.SetActive(true);
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
        storePanel.SetActive(false);
    }

    public void UpgradeImmortalityBtn()
    {
        UpgradePowerup(immortality);
    }

    public void UpgradeMagnetBtn()
    {
        UpgradePowerup(magnet);
    }

    private void UpgradePowerup(Powerup powerup)
    {
        if (coins >= powerup.GetNextUpgradeCost() && powerup.IsMaxedOut() == false)
        {
            coins -= powerup.GetNextUpgradeCost();
            PlayerPrefs.SetInt(PlayerPrefsKeys.COINS, coins);

            powerup.Upgrade();
            UpdateUI();
        }
    }
}
