using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerActions playerInputs;
    public PlayerAim aim {  get; private set; }
    public PlayerMovement movement { get; private set; }

    private void Awake()
    {
        playerInputs = new PlayerActions();
        aim = GetComponent<PlayerAim>();
        movement = GetComponent<PlayerMovement>();
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
