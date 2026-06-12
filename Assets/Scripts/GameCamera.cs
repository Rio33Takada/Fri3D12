using UnityEngine;
using UnityEngine.InputSystem;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] Transform lookTarget;
    [SerializeField] Vector3 offset;
    [SerializeField] float targetDistance;
    [SerializeField] float rotateSpeed;

    float pitch;
    float yaw;

    private void Start()
    {
        pitch = 90;
        yaw = 0;
    }

    private void Update()
    {
        var lookVec = playerInput.actions["Look"].ReadValue<Vector2>();
        yaw += lookVec.x * rotateSpeed * Time.deltaTime;
        pitch -= lookVec.y * rotateSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        var target = lookTarget.position + offset;
        var rotation = Quaternion.Euler(pitch, yaw, 0);
        var position = rotation * new Vector3(0, 0, -targetDistance)  + target;
        transform.rotation = rotation;
        transform.position = position;
    }
}
