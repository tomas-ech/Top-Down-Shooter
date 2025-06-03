using UnityEngine;

public static class AnimationVariables
{
    public static readonly int XMovement = Animator.StringToHash("xMovement");
    public static readonly int ZMovement = Animator.StringToHash("zMovement");
    public static readonly int WeaponType = Animator.StringToHash("WeaponType");

    public static readonly int IsRunning = Animator.StringToHash("isRunning");
    public static readonly int IsBusyGrabbing = Animator.StringToHash("isBussyGrabbing");

    public static readonly int Fire = Animator.StringToHash("Fire");
    public static readonly int Reload = Animator.StringToHash("Reload");
    public static readonly int Grab = Animator.StringToHash("Grab");
}
