using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : ScriptableObject
{
    public bool isActive;
    [SerializeField]
    protected PowerupStats duration;

    [SerializeField]
    protected int currentLevel = 1;

    [SerializeField]
    protected int maxLevel = 3;

    [SerializeField]
    protected int[] UpgradeCosts;

    public float GetDuration()
    {
        return duration.GetValue(currentLevel);
    }

    private void Awake()
    {
        LoadPowerupLevel();
    }

    private void LoadPowerupLevel()
    {
        string key = name + "Level";
        currentLevel = PlayerPrefs.GetInt(key, 1);
    }

    private void SavePowerupLevel()
    {
        string key = name + "Level";
        PlayerPrefs.SetInt(key, currentLevel);
    }

    public bool IsMaxedOut()
    {
        return currentLevel == maxLevel;
    }

    public int GetNextUpgradeCost()
    {
        if(IsMaxedOut() == false)
        {
            return UpgradeCosts[currentLevel - 1];
        }
        else
        {
            return -1;
        }
    }

    public void Upgrade()
    {
        if (IsMaxedOut())
        {
            return;
        }

        currentLevel++;

        SavePowerupLevel();
    }

    public override string ToString()
    {
        string text = $"{name}\nLVL. {currentLevel}";
        if (IsMaxedOut())
        {
            text += " (MAX)";
        }
        return text;
    }

    public string UpgradeCostString()
    {
        if(IsMaxedOut() == false)
        {
            return $"UPGRADE\nCOST: {GetNextUpgradeCost()}";
        }
        else
        {
            return "MAXED OUT";
        }
    }
}