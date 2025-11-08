using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터와 충돌 감지
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy encountered! Starting battle...");

            // 씬 전환 (카드 전투 씬 이름에 맞게 수정)
            SceneManager.LoadScene("Battle Scene");
        }
    }
}
