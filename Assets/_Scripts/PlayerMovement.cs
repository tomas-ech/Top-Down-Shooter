using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private LayerMask aimMask;

    private PlayerActions playerInputs;
    private CharacterController characterController;
    private Animator animator;
    private Player player;

    private Vector3 movementDirection;
    private Vector2 movementInput;

    private Vector3 aimDirection;
    private Vector2 aimInput;

    private float verticalVelocity;
    private bool isRunning;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();

        speed = walkSpeed;

        AssignInputEvents();
    }

    private void Update()
    {
        SetMovement();
        SetAim();
        AnimatorControllers();
    }

    private void AnimatorControllers()
    {
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        animator.SetFloat(AnimationVariables.xMovement, xVelocity, 0.1f, Time.deltaTime);
        animator.SetFloat(AnimationVariables.zMovement, zVelocity, 0.1f, Time.deltaTime);

        bool useRunAnimation = isRunning && movementDirection.magnitude > 0;
        animator.SetBool(AnimationVariables.isRunning, useRunAnimation);
    }

    private void SetAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimMask))
        {
            aimDirection = hitInfo.point - transform.position;
            aimDirection.y = 0f;
            aimDirection.Normalize();

            transform.forward = aimDirection;
        }
    }

    private void SetMovement()
    {
        movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        ApplyGravity();

        if (movementDirection.magnitude > 0)
        {
            characterController.Move(speed * Time.deltaTime * movementDirection);
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            movementDirection.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = -0.5f;
        }
    }

    #region Input System
    private void AssignInputEvents()
    {
        playerInputs = player.playerInputs;

        playerInputs.Character.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        playerInputs.Character.Movement.canceled += context => movementInput = Vector2.zero;

        playerInputs.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerInputs.Character.Aim.canceled += context => aimInput = Vector2.zero;

        playerInputs.Character.Run.performed += context =>
        {
            isRunning = true;
            speed = runSpeed;

        };

        playerInputs.Character.Run.canceled += context =>
        {
            isRunning = false;
            speed = walkSpeed;
        };
    }
    #endregion
}
