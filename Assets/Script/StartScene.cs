using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene"); // 다음 씬으로 이동
    }

    public void OpenOption()
    {
        Debug.Log("옵션창 열기 (나중에 구현)");
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");

        // 에디터 환경에서는 종료가 안 되므로 조건 분기
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 플레이 모드 중지
#else
        Application.Quit(); // 빌드된 실행 파일에서는 실제로 종료됨
#endif
    }
}
