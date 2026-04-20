using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour
{
    private Animator _animator;
    private Collider2D _collider;

    private bool _collected;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected) return;

        if (!collision.TryGetComponent<IItemReceiver>(out var receiver))
            return;

        _collected = true;

        receiver.OnCollect();

        StartCoroutine(CollectRoutine());
    }

    private IEnumerator CollectRoutine()
    {
        // Deactivate collision
        _collider.enabled = false;

        // Play animation
        _animator.SetTrigger("Collected");

        // Wait for the anim to end
        yield return new WaitForSeconds(.5f);

        // Destroy object
        Destroy(gameObject);
    }
}
