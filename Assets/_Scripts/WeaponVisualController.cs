using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] weaponsTransform;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private Transform currentWeapon;

    [Header("Left Hand IK")]
    [SerializeField] private Transform leftHand;

    private void Start()
    {
        SwitchWeaponON(pistol);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeaponON(pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeaponON(revolver);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeaponON(autoRifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeaponON(shotgun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeaponON(rifle);
        }
    }

    private void SwitchWeaponON(Transform weapon)
    {
        SwitchWeaponsOFF();
        weapon.gameObject.SetActive(true);
        currentWeapon = weapon;

        AttachLeftHand();
    }

    private void SwitchWeaponsOFF()
    {
        for (int i = 0; i < weaponsTransform.Length; i++)
        {
            weaponsTransform[i].gameObject.SetActive(false);
        }
    }

    private void AttachLeftHand()
    {
        Transform target = currentWeapon.GetComponentInChildren<LeftHand>().transform;

        leftHand.localPosition = target.localPosition;
        leftHand.localRotation = target.localRotation;
    }
}
