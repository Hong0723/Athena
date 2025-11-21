using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnHitpt : MonoBehaviour
{
    [SerializeField] private string sceneName = "Battle Scene";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
