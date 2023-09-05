using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    private Animator animator;

    public static Action PlayerDead;
    public static Action<int> PlayerDamaged;


    private void Start()
    {
        health = 3;
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DecreaseHealth();

            if (PlayerIsDead())
                return;

            StartCoroutine(IgnoreCollisions(collision));
        }
    }

    private IEnumerator IgnoreCollisions(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSecondsRealtime(1.3f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    private void DecreaseHealth()
    {
        animator.SetTrigger("PlayerHurt");
        health -= 1;

        PlayerDamaged.Invoke(health);
    }

    private bool PlayerIsDead()
    {
        if (health <= 0)
        {
            health = 0;
            PlayerDead.Invoke();

            return true;
        }

        return false;
    }
}
