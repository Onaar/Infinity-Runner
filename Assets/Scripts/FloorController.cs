using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject floorTile1, floorTile2;
    public GameObject[] tiles;
    int licznik = 2;
    int newTileIndex;

    private void FixedUpdate()
    {
        if (!GameManager.gameManager.inGame) return;
        float speed = GameManager.gameManager.worldScrollingSpeed;
        floorTile1.transform.position -= new Vector3(speed, 0f, 0f);
        floorTile2.transform.position -= new Vector3(speed, 0f, 0f);
        if(floorTile2.transform.position.x <= 7.5f)
        {
            if (licznik == 1)
            {
                newTileIndex = Random.Range(0, 4);
                licznik = 2;
            }
            else
            {
                newTileIndex = 4;
                licznik = 1;
            }

            var newTile = Instantiate(tiles[newTileIndex], floorTile2.transform.position + new Vector3(16f, 0f, 0f), Quaternion.identity);

            Destroy(floorTile1);

            floorTile1 = floorTile2;
            floorTile2 = newTile;
        }
    }
}
