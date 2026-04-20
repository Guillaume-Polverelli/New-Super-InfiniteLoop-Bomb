using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerState : MonoBehaviour
{
    public event Action OnPlayerDeath;

    private void Start()
    {
        GameManager.RegisterPlayer(this);
    }

    void Die()
    {
        StartCoroutine(DyingRoutine());

        OnPlayerDeath?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IkillZone>() != null) 
        {
            Die();
        }
    }

    private IEnumerator DyingRoutine()
    {
        //We disable control but keep GameObject active
        GetComponent<Controller.PlayerController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        //Stop Physics movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Stop physic
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;

            // Freeze player
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Play animation for player 
        Animator playerAnimator = GetComponent<Animator>();
        if (playerAnimator != null)
            playerAnimator.SetTrigger("HasDied");

        yield return new WaitForSeconds(.7f);

        // Destroy object
        Destroy(gameObject);
    }
}
