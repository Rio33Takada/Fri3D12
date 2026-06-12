using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedMax;

    PlayerInput playerInput;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        var moveVec = playerInput.actions["Move"].ReadValue<Vector2>();

        var cameraDir = playerInput.camera.transform.forward;

        cameraDir.y = 0;

        cameraDir = cameraDir.normalized;

        var cameraRight = playerInput.camera.transform.right;

        var moveVec3D = (cameraDir * moveVec.y + cameraRight * moveVec.x) * speedMax;

        transform.position += moveVec3D * Time.deltaTime;

        transform.forward = Vector3.Lerp(transform.forward, moveVec3D.normalized, 0.1f);
    }
}
