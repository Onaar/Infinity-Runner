using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float featherFallingForce;

    public bool jumped;
    public bool doubleJumped;

    public LayerMask whatIsTheGround;

    Rigidbody2D rb2d;
    float timestamp;
    PolygonCollider2D polygonCollider2D;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (!GameManager.gameManager.inGame) return;
        ResetJumpFlags();
        Jump();
        ApplyLiftingForce();
    }

    private void ResetJumpFlags()
    {
        if (IsGrounded() && Time.time >= timestamp)
        {
            if (jumped || doubleJumped)
            {
                jumped = false;
                doubleJumped = false;
            }

            timestamp = Time.time + 1f;
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!jumped)
            {
                rb2d.velocity = new Vector2(0f, jumpForce);
                jumped = true;
                SoundManager.instance.PlayOnceJump();
            }
            else if (!doubleJumped)
            {
                rb2d.velocity = new Vector2(0f, jumpForce);
                doubleJumped = true;
                SoundManager.instance.PlayOnceJump();
            }
        }
    }

    private void ApplyLiftingForce()
    {
        if (Input.GetMouseButton(0) && rb2d.velocity.y <= 0)
        {
            rb2d.AddForce(new Vector2(0f, featherFallingForce * Time.deltaTime));
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
                           polygonCollider2D.bounds.center,
                           polygonCollider2D.bounds.size,
                           0f,
                           Vector2.down,
                           0.1f,
                           whatIsTheGround);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" && !GameManager.gameManager.immortality.isActive)
        {
            PlayerDeath();
        }
        else if (collision.tag == "Coin")
        {
            GameManager.gameManager.CoinCollected();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Immortality")
        {
            GameManager.gameManager.ImmortalityCollected();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Magnet")
        {
            GameManager.gameManager.MagnetCollected();
            Destroy(collision.gameObject);
        }
    }

    private void PlayerDeath()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.gameManager.GameOver();
    }
}
