using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] 
    private Canvas myInventory;//인벤토리 canvas
    private bool isWatchInventory;//인벤토리 껏다켰다
    public List<GameObject> itemslots;//아이템 슬롯(배경)의 게임오브젝트 리스트
    public static Inventory Instance { get; private set; }

    void Awake()
    {
        
        //싱글톤
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동 시에도 유지 (원하면 삭제 가능)

        itemslots = new List<GameObject>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isWatchInventory = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isWatchInventory)
            {
                myInventory.gameObject.SetActive(false);
                isWatchInventory = false;
            }
            else
            {
                myInventory.gameObject.SetActive(true);
                isWatchInventory = true;
            }
                
        }
    }

    //itemslot에서 호출합니다
    public void Enroll(GameObject gameObject)
    {
        itemslots.Add(gameObject);
    }

    public bool CheckImagePosition(Vector3 releasedPos , GameObject gameobject)
    {
        foreach(GameObject slot in itemslots){

            Collider col = slot.GetComponent<Collider>();
            if (col == null)
                continue;

            // Collider2D의 Bounds 영역 안에 있는지 체크
            //아이템 슬롯의 범위내에 아이템을 놔두었을 경우 알아서 중앙에 배치되도록 하려고
            if (col.bounds.Contains(releasedPos))
            {
                Debug.Log($" {slot.name} 슬롯 안에 아이템이 놓였습니다!");
                RectTransform rect = gameobject.GetComponent<RectTransform>();
                RectTransform slotRect = slot.GetComponent<RectTransform>(); // 추가

                // 부모를 슬롯으로 변경
                rect.SetParent(slotRect, worldPositionStays: false);

                // 로컬 위치를 0으로 맞추면 슬롯 중앙에 정렬됨
                rect.anchoredPosition = Vector2.zero;
                
                rect.position = slot.transform.position;//11.13
                return true;
            }
        }
        Debug.Log($"Item 오브젝트가 놓인 위치: {releasedPos}");
        return false;        
    }
}
