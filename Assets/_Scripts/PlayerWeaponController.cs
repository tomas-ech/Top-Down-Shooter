using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        player.playerInputs.Character.Shoot.performed += context => Shoot(); 
    }

    private void Shoot()
    {
        Animator animator = GetComponentInChildren<Animator>();

        animator.SetTrigger(AnimationVariables.fire);
    }
}
