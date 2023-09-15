using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyController : MonoBehaviour
{
    public static Action KeyPickedUp;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            KeyPickedUp.Invoke();
            animator.SetTrigger("KeyCollected");
            WaitBeforeDestroy();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
