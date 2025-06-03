using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private WeaponVisualController weaponVisualController;

    private void Start()
    {
        weaponVisualController = GetComponentInParent<WeaponVisualController>();
    }

    public void ActivateRig()
    {
        weaponVisualController.ChangeRigWeigth();
    }
}
