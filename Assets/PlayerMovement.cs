using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private LayerMask aimMask;

    private PlayerActions playerInputs;
    private CharacterController characterController;
    private Animator animator;

    private float verticalVelocity;

    private Vector3 movementDirection;
    private Vector2 movementInput;
    private Vector2 aimInput;
    private Vector3 aimDirection;

    private void Awake()
    {
        playerInputs = new PlayerActions();

        playerInputs.Character.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        playerInputs.Character.Movement.canceled += context => movementInput = Vector2.zero;

        playerInputs.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerInputs.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
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

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
