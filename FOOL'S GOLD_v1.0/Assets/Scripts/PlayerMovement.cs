
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Animator playerAnimator;
    [SerializeField] ParticleSystem runSys;
    [SerializeField] GameObject miningInfoBox;

    Vector2 movement;
    float moveSpeed = 9.75f;

    void Awake()
    {
        runSys.Play();
    }

    void Update()
    {
        CharMovement();

        if (miningInfoBox.activeInHierarchy)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 15;
        }
    }

    void FixedUpdate()
    {
        Vector3 playerWorldPos = playerRb.transform.position;

        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);


    }

    void CharMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
    }
}