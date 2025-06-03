using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponVisualController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform[] weaponsTransform;

    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform rifle;

    private Transform currentWeapon;
    private Rig rig;
    private bool canRigIncrease = false;

    [Header("Rig")]
    [SerializeField] private float rigIncrease = 2f;

    [Header("Left Hand IK")]
    [SerializeField] private Transform leftHand;

    private void Start()
    {
        SwitchWeaponON(pistol);

        animator = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
    }

    private void Update()
    {
        CheckWeaponSwitch();

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger(AnimationVariables.Reload);
            rig.weight = 0.15f;
        }

        if(canRigIncrease)
        {
            rig.weight += rigIncrease * Time.deltaTime;
        }
    }

    public void ChangeRigWeigth() => canRigIncrease = true;

    private void CheckWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeaponON(pistol);
            SwitchAnimationLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeaponON(revolver);
            SwitchAnimationLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeaponON(autoRifle);
            SwitchAnimationLayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeaponON(shotgun);
            SwitchAnimationLayer(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeaponON(rifle);
            SwitchAnimationLayer(3);
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

    private void SwitchAnimationLayer(int layerIndex)
    {
        for (int i = 1; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(layerIndex, 1);
    }
}
