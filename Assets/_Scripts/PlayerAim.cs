
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Player player;
    private PlayerActions playerInputs;

    [Header("Aim Info")]
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private Transform aimTarget;
    private Vector2 aimInput;

    private void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    private void Update()
    {
        aimTarget.position = new Vector3(GetMousePosition().x, transform.position.y + 1, GetMousePosition().z); 
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimMask))
        {
            return hitInfo.point;
        }

        return Vector3.zero;
    }

    private void AssignInputEvents()
    {
        playerInputs = player.playerInputs;

        playerInputs.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerInputs.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }
}
