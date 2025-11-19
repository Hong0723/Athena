using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplainItemToText : MonoBehaviour
{

    public static ExplainItemToText Instance { get; private set; }
    // 아이템 설명 관련
    [TextArea] public string itemDescription; // 인스펙터에서 설명 입력 가능
    public TextMeshProUGUI descriptionTextUI; // 설명이 표시될 UI Text (예: 화면 하단 텍스트 박스)

    private void Awake()
    {
        // 이미 인스턴스가 존재한다면 중복 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스 등록
        Instance = this;

        // 씬 전환 시 파괴되지 않도록 (필요할 때만)
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        descriptionTextUI.gameObject.SetActive(false);//텍스트를 숨김상태로 시작
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDescription(string description)
    {
        
        if (descriptionTextUI != null)
        {
            Debug.LogWarning($"[ItemManager] descriptionTextUI가 설정");
            descriptionTextUI.text = description;
            descriptionTextUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[ItemManager] descriptionTextUI가 설정되지 않음: {name}");
        }
    }

    //설명 숨기기 함수
    public void HideDescription()
    {
        
        if (descriptionTextUI != null)
        {
            descriptionTextUI.gameObject.SetActive(false);
        }
        
    }
}
