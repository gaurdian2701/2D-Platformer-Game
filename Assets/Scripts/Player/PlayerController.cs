﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 newCrouchColliderOffset;
    [SerializeField] private Vector2 newCrouchColliderSize;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float knockbackForce;


    private Rigidbody2D rb;

    private Vector2 originalColliderOffset;
    private Vector2 originalColliderSize;
    private CapsuleCollider2D playerCollider;
    private float horizontalInput;

    private enum PlayerState
    {
        grounded,
        falling,
        jumping,
        dead
    };

    private PlayerState playerState;

    private void Start()
    {
        horizontalInput = 0f;

        playerCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        originalColliderOffset = playerCollider.offset;
        originalColliderSize = playerCollider.size;

        playerState = PlayerState.grounded;

        PlayerHealth.PlayerDamaged += KnockBackPlayer;
        PlayerHealth.PlayerDead += KillPlayer;
    }

    private void Update()
    {
        if (playerState == PlayerState.dead)
            return;

        CheckForFalling();
        GetPlayerInput();
        CheckDirection();
    }

    private void OnDestroy()
    {
        PlayerHealth.PlayerDamaged -= KnockBackPlayer;
        PlayerHealth.PlayerDead -= KillPlayer;
    }

    private void GetPlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        MovePlayer(horizontalInput);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("PlayerCrouching", true);
            ResizeCollider();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("PlayerCrouching", false);
            RestoreColliderSize();
        }
    }

    private void PlayMoveAnimation() => animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

    private void PlayJumpAnimation() => playerState = PlayerState.jumping;
    
  

    private void MovePlayer(float inputHorizontal)
    {
        Vector3 position = transform.position;

        position.x += inputHorizontal * moveSpeed * Time.deltaTime;
        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            PlayJumpAnimation();
        }

        else if(IsGrounded())
            PlayMoveAnimation();

        animator.SetInteger("PlayerState", (int)playerState);
    }

    private void KnockBackPlayer(int health) => rb.AddForce(new Vector2(-transform.forward.z, knockbackForce), ForceMode2D.Impulse);

    private void KillPlayer()
    {
        animator.SetTrigger("PlayerDead");
        playerState = PlayerState.dead;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }

    private void RestoreColliderSize()
    {
        playerCollider.offset = originalColliderOffset;
        playerCollider.size = originalColliderSize;
    }

    private void ResizeCollider()
    {
        playerCollider.offset = newCrouchColliderOffset;
        playerCollider.size = newCrouchColliderSize;
    }

    private void CheckDirection()
    {
        if (horizontalInput < 0f)
            transform.eulerAngles = new Vector3(0, 180, 0);

        else if (horizontalInput > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void CheckForFalling()
    {
        if (!IsGrounded() && rb.velocity.y < 0f)
            playerState = PlayerState.falling;

        animator.SetInteger("PlayerState", (int)playerState);
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(playerCollider.bounds.center, -transform.up, 2.2f, LayerMask.GetMask("Platform")))
        {
            playerState = PlayerState.grounded;
            animator.SetInteger("PlayerState", (int)playerState);
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        RaycastHit2D rayhit = Physics2D.Raycast(playerCollider.bounds.center, -transform.up, 2.3f, LayerMask.GetMask("Platform"));

        if (rayhit)
            Gizmos.color = Color.red;
    
        Gizmos.DrawRay(playerCollider.bounds.center, -transform.up);
    }
}