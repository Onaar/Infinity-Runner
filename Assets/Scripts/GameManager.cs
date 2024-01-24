using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; // default = null

    public float worldScrollingSpeed = .2f;
    public Text scoreText;
    public Text coinText;
    private float score;

    public bool inGame;
    public GameObject resetButton;
    public GameObject menuButton;

    private int coins;

    //TODO-EXTRA: Zapisywanie najlepszego wyniku

    public Immortality immortality;
    public Magnet magnet;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        immortality.isActive = false;
        magnet.isActive = false;

        inGame = true;
        resetButton.SetActive(false);
        menuButton.SetActive(false);
        GetCoins();
        UpdateCoinsOnScreen();
    }

    private void GetCoins()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.COINS))
        {
            coins = PlayerPrefs.GetInt(PlayerPrefsKeys.COINS);
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt(PlayerPrefsKeys.COINS, coins);
        }
    }

    private void FixedUpdate()
    {
        if (!inGame) return;
        score += worldScrollingSpeed;
        UpdateScoreOnScreen();
        UpdateCoinsOnScreen();
    }
    private void UpdateScoreOnScreen()
    {
        scoreText.text = score.ToString("0");
    }

    private void UpdateCoinsOnScreen()
    {
        coinText.text = coins.ToString();
    }

    public void GameOver()
    {
        inGame = false;
        resetButton.SetActive(true);
        menuButton.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CoinCollected(int value = 1)
    {
        coins += value;
        PlayerPrefs.SetInt(PlayerPrefsKeys.COINS, coins);
        SoundManager.instance.PlayOnceCoinGrab();
    }

    public void ImmortalityCollected()
    {
        if (immortality.isActive)
        {
            CancelInvoke("CancelImmortality");
            CancelImmortality();
        }

        immortality.isActive = true;
        worldScrollingSpeed += immortality.GetSpeedBoost();
        Invoke("CancelImmortality", immortality.GetDuration());
    }

    public void CancelImmortality()
    {
        immortality.isActive = false;
        worldScrollingSpeed -= immortality.GetSpeedBoost();
    }

    public void MagnetCollected()
    {
        if (magnet.isActive)
        {
            CancelInvoke("CancelMagnet");
            CancelMagnet();
        }

        magnet.isActive = true;
        Invoke("CancelMagnet", magnet.GetDuration());
    }
    public void CancelMagnet()
    {
        magnet.isActive = false;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
