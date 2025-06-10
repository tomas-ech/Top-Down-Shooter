
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Player player;
    private PlayerActions playerInputs;

    [Header("Aim Info")]
    [SerializeField] private Transform aimTarget;
    [SerializeField] private bool isAimPrecise;

    [Header("Camera Info")]
    [SerializeField] private Transform cameraTarget;

    [Range(1.5f, 3f)]
    [SerializeField] private float maxCameraDistance = 4f;

    [Range(0.5f, 1.5f)]
    [SerializeField] private float minCameraDistance = 1.5f;

    [Range(3f, 5f)]
    [SerializeField] private float cameraSensitivity = 5f;

    [Space]

    [SerializeField] private LayerMask aimMask;

    private Vector2 aimInput;
    private RaycastHit lastMouseHit;

    private void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    private void Update()
    {
        UpdateAimPosition();
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        cameraTarget.position = Vector3.Lerp(cameraTarget.position, DesiredCameraPosition(), cameraSensitivity * Time.deltaTime);
    }

    private void UpdateAimPosition()
    {
        aimTarget.position = GetMouseHitInfo().point;

        if (!isAimPrecise)
        {
            aimTarget.position = new Vector3(GetMouseHitInfo().point.x, transform.position.y + 1, GetMouseHitInfo().point.z);
        }
    }

    public bool CanAimPrecise()
    {
         return isAimPrecise;
    }

    private Vector3 DesiredCameraPosition()
    {
        float actualMaxCameraDistance = player.movement.movementInput.y < -0.5f ? maxCameraDistance : minCameraDistance;

        Vector3 desiredCameraPosition = GetMouseHitInfo().point;
        Vector3 aimDirection = (desiredCameraPosition - transform.position).normalized;

        float distanceToDesiredPosition = Vector3.Distance(transform.position, desiredCameraPosition);
        float clampDistance = Mathf.Clamp(distanceToDesiredPosition, minCameraDistance, actualMaxCameraDistance);

        desiredCameraPosition = transform.position + aimDirection * clampDistance;
        desiredCameraPosition.y = transform.position.y + 1;

        return desiredCameraPosition;
    }

    public RaycastHit GetMouseHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimMask))
        {
            lastMouseHit = hitInfo;
            return hitInfo;
        }

        return lastMouseHit;

    }

    private void AssignInputEvents()
    {
        playerInputs = player.playerInputs;

        playerInputs.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerInputs.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }
}
