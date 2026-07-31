using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private Text counterText;

    private int enemyCount;

    [SerializeField] private int maxCount;

    private void Awake()
    {
        enemyCount = maxCount;

        UpdateText();
    }

    public void EnemyDeath()
    {
        enemyCount--;
        UpdateText();
    }

    private void UpdateText()
    {
        if (enemyCount == 0)
        {
            counterText.text = "GameClear";
        }
        else
        {
            counterText.text = $"{enemyCount}/{maxCount}";
        }
    }
}
