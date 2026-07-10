using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speedMax = 5;
    [SerializeField] private float accel = 60;
    [SerializeField] private float rotateSpeed = 20;
    [SerializeField] private float jumpSpeed = 12;
    [SerializeField] private float groundNormalYMin = 0.7f;
    [SerializeField] private float groundDamping = 8f;
    [SerializeField] private float airDamping = 0.2f;


    private PlayerInput playerInput;
    private Rigidbody rb;
    private Vector3 rotateTarget;
    private bool isGrounded;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = -1;
    }

    private void Update()
    {
        //var moveVec = playerInput.actions["Move"].ReadValue<Vector2>();

        //var cameraDir = playerInput.camera.transform.forward;

        //cameraDir.y = 0;

        //cameraDir = cameraDir.normalized;

        //var cameraRight = playerInput.camera.transform.right;

        //var moveVec3D = (cameraDir * moveVec.y + cameraRight * moveVec.x) * speedMax;

        //transform.position += moveVec3D * Time.deltaTime;

        //transform.forward = Vector3.Lerp(transform.forward, moveVec3D.normalized, 0.1f);

        if (isGrounded)
        {


            var accelVec = playerInput.actions["Move"].ReadValue<Vector2>();

            var cameraDir = playerInput.camera.transform.forward;
            cameraDir.y = 0;
            cameraDir = cameraDir.normalized;

            var cameraRight = playerInput.camera.transform.right;

            var accelVec3D =
                cameraDir * accelVec.y * accel
                + cameraRight * accelVec.x * accel;
            rb.AddForce(accelVec3D, ForceMode.Acceleration);

            // プレイヤーの向きを変える
            if (accelVec3D != Vector3.zero)
            {
                rotateTarget = accelVec3D.normalized;
            }
            // 前方向をコピーしておく
            Vector3 forward = transform.forward;



            // 上方向を固定
            transform.up = Vector3.up;



            // 前方向をターゲットに向かって補間

            var tempForward = Vector3.Slerp(forward, rotateTarget, rotateSpeed * Time.deltaTime);

            if (tempForward != Vector3.zero)
            {
                transform.forward = tempForward;
            }


            // アニメーターのMoveSpeedパラメータに Rigidbody の移動速度の大きさを与える
            Vector3 velocityXZ = rb.linearVelocity;
            velocityXZ.y = 0;
            animator.SetFloat("MoveSpeed", velocityXZ.magnitude);

            // ジャンプ
            if (playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded)
            {
                Vector3 jumpVec = new Vector3(0, jumpSpeed, 0);
                rb.AddForce(jumpVec, ForceMode.VelocityChange);
            }
        }
    }

    private void FixedUpdate()
    {
        // 減衰を地上と空中で変える
        if (isGrounded)
        {
            rb.linearDamping = groundDamping;
        }
        else
        {
            rb.linearDamping = airDamping;
        }

        // 物理計算中に接地判定を行うため、一旦ここで false にしておく
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y >= groundNormalYMin)
            {
                isGrounded = true;
            }
        }
    }

}
