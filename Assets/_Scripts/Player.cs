using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerActions playerInputs;
    public PlayerAim aim {  get; private set; }

    private void Awake()
    {
        playerInputs = new PlayerActions();
        aim = GetComponent<PlayerAim>();
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
