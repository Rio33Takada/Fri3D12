using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotateSpeed = 3;
    [SerializeField] private float sightRange = 3;
    [SerializeField] private float sightAngle = 45;
    [SerializeField] private int hp = 2;
    [SerializeField] private float invincibleTimeMax = 0.5f;
    [SerializeField] private float knockbackSpeed = 5;

    public Collider playerCollider { get; set; }

    private Rigidbody rb;
    private float invincibleTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var target = playerCollider.bounds.center;

        target.y = transform.position.y;

        var direction = target - transform.position;
        if(CanFindPlayer(target))
        {
            var currentVelocityY = rb.linearVelocity.y;

            var newVelocity = direction.normalized * moveSpeed;

            newVelocity.y = currentVelocityY;

            rb.linearVelocity = newVelocity;

            var forward = transform.forward;

            // 上方向を固定
            transform.up = Vector3.up;

            // 前方向をターゲットに向かって補間

            var tempForward = Vector3.Slerp(forward, direction.normalized, rotateSpeed);

            if (tempForward != Vector3.zero)
            {
                transform.forward = tempForward;
            }

            if (invincibleTime > 0)
            {
                invincibleTime -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        var attackObj = collision.gameObject.GetComponent<AttackObject>();
        if (attackObj != null && invincibleTime <= 0)
        {
            hp -= attackObj.power;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool CanFindPlayer(Vector3 target)
    {
        var direction = target - rb.position;
        var origin = transform.position + Vector3.up * 1.5f;
        bool isSeenPlayer = true;
        if (Physics.Raycast(origin, direction.normalized,
            out var hitInfo, sightRange))
        {
            if (hitInfo.collider != playerCollider)
            {
                // プレイヤー以外の障害物に当たった場合は見えない1
                isSeenPlayer = false;
            }
        }

        var angle = Vector3.Angle(transform.forward, direction);
        var distance = direction.magnitude;

        var insideOfSight = angle <= sightAngle && distance <= sightRange;

        return isSeenPlayer && insideOfSight;
    }
}
