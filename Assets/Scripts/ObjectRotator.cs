using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 2f;

    private void Start()
    {
        rotationSpeed = Random.Range(0.5f * rotationSpeed, 1.5f * rotationSpeed);
    }

    private void FixedUpdate()
    {
        if (!GameManager.gameManager.inGame) return;
        transform.Rotate(new Vector3(0,0,rotationSpeed));
    }
}
