using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magnet", menuName = "Powerup/Magnet")]
public class Magnet : Powerup
{
    [SerializeField]
    private PowerupStats range;
    [SerializeField]
    private PowerupStats coinSpeed;

    public float GetRange()
    {
        return range.GetValue(currentLevel);
    }

    public float GetCoinSpeed()
    {
        return coinSpeed.GetValue(currentLevel);
    }
}
