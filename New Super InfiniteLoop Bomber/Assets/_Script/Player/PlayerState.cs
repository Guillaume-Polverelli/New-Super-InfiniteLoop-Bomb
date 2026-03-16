using System;
using System.Linq;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public event Action OnPlayerDeath;

    private void Start()
    {
        GameManager.RegisterPlayer(this);
    }
    void Die()
    {
        OnPlayerDeath?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IkillZone>() != null) 
        {
            Die();
        }
    }
}
