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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeaponON(pistol);
        }
    }

    private void SwitchWeaponON(Transform weapon)
    {
        SwitchWeaponsOFF();
        weapon.gameObject.SetActive(true);
    }
    
    private void SwitchWeaponsOFF()
    {
        for (int i = 0; i < weaponsTransform.Length; i++)
        {
            weaponsTransform[i].gameObject.SetActive(false);
        }
    }
}
