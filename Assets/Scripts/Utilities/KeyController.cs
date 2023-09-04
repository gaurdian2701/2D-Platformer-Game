using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyController : MonoBehaviour
{
    public static Action KeyPickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            KeyPickedUp.Invoke();

        Destroy(this.gameObject);
    }
}
