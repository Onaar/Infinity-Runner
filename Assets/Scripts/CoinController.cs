using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Transform player;
    GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameManager.gameManager;
    }

    private void FixedUpdate()
    {
        if (!gameManager.magnet.isActive) return;

        float distance = Vector3.Distance(transform.position, player.position),
            range = gameManager.magnet.GetRange();

        if (distance <= range)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float coinSpeed = gameManager.magnet.GetCoinSpeed();
            transform.position += direction * coinSpeed;
        }
    }
}
