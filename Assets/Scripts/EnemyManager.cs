using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;

    [SerializeField] Enemy[] enemies;

    private void OnEnable()
    {
        foreach (var enemy in enemies)
        {
            enemy.PlayerCollider = playerCollider;
        }
    }
}
