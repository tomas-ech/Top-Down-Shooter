using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerWeaponVisuals weaponVisualController;

    private void Start()
    {
        weaponVisualController = GetComponentInParent<PlayerWeaponVisuals>();
    }

    public void ActivateRig()
    {
        weaponVisualController.ChangeRigWeigthToOne();
    }

    public void WeaponGrabIsOver()
    {
        weaponVisualController.ChangeRigWeigthToOne();
        weaponVisualController.ChangeRigIKWeigthToOne();
    }


}
