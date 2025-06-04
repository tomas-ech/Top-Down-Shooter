using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponVisuals : MonoBehaviour
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
    private bool canRigIKIncrease = false;

    [Header("Rig")]
    [SerializeField] private float rigIncrease = 2f;

    [Header("Left Hand IK")]
    [SerializeField] private Transform leftHand;
    [SerializeField] private TwoBoneIKConstraint leftHand_IK;

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
            PauseRig();
        }

        if (canRigIncrease)
        {
            rig.weight += rigIncrease * Time.deltaTime;

            if (rig.weight >= 1)
            {
                canRigIncrease = false;
            }
        }
        
        if (canRigIKIncrease)
        {
            leftHand_IK.weight += rigIncrease * Time.deltaTime;

            if (leftHand_IK.weight >= 1)
            {
                canRigIKIncrease = false;
            }
        }
    }

    private void PauseRig()
    {
        rig.weight = 0.15f;
    }

    public void PlayWeaponGrab(GrabType grabType)
    {
        leftHand_IK.weight = 0;
        PauseRig();
        animator.SetFloat(AnimationVariables.WeaponType, ((float)grabType));
        animator.SetTrigger(AnimationVariables.Grab);
    }

    public void ChangeRigWeigthToOne() => canRigIncrease = true;
    public void ChangeRigIKWeigthToOne() => canRigIKIncrease = true;

    private void CheckWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeaponON(pistol);
            SwitchAnimationLayer(1);
            PlayWeaponGrab(GrabType.SideGrab);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeaponON(revolver);
            SwitchAnimationLayer(1);
            PlayWeaponGrab(GrabType.BackGrab);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeaponON(autoRifle);
            SwitchAnimationLayer(1);
            PlayWeaponGrab(GrabType.BackGrab);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeaponON(shotgun);
            SwitchAnimationLayer(2);
            PlayWeaponGrab(GrabType.BackGrab);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeaponON(rifle);
            SwitchAnimationLayer(3);
            PlayWeaponGrab(GrabType.SideGrab);
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

public enum GrabType
{
    BackGrab,
    SideGrab
};