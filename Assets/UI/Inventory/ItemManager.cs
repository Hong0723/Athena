using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
{
  
    public RectTransform rectTransform;
    public Vector3 originalPosition;
    public Camera mainCam;
    public bool isDragging = false;

    public GameObject activeButton;
    public GameObject DeleteButton;
    private bool isEnterMouse;

    //ScriptableObject 아이템 데이터 연결
    [Header("Item Data")]
    public ItemData itemData;

    private Canvas dragCanvas; // 드래그 중에 임시로 붙는 Canvas

    void Start()
    {
        Image img = GetComponent<Image>();
        // 완전히 투명해도 감지되게 (0은 완전 투명도까지 감지)
        img.alphaHitTestMinimumThreshold = 0.0f;
        originalPosition = rectTransform.position;
        isEnterMouse = false;
        
        //아이템 아이콘 표시
        if (itemData != null)
        {
            img.sprite = itemData.itemIcon;
        }      
    }

  
    public void OnPointerDown(PointerEventData eventData)
    {     
        Debug.Log("OnPointerDown");
        isDragging = true;

        if (eventData.pointerPress == activeButton)
        {
            Debug.Log("ActiveButton 클릭됨!");
        }
        else if (eventData.pointerPress == DeleteButton)
        {
            Debug.Log("DeleteButton 클릭됨!");
        }
        //  드래그 시작 시 버튼 숨김
        activeButton.SetActive(false);
        DeleteButton.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        isDragging = false;

        //현재 스크립트가 붙은 오브젝트의 좌표를 전달
        Vector3 releasedPos = transform.position;        
        bool isChangeSlot = Inventory.Instance.CheckImagePosition(releasedPos, gameObject);//아이템이 다른슬롯안으로 진입했는지여부

        if (isChangeSlot)
        {
            //이미 CheckImagePosition에서 위치변경해줌
            originalPosition=rectTransform.position;
        }
        else
        {
            //원래위치로 복귀
            rectTransform.position = originalPosition;
        }

        //  드래그가 끝나면 마우스가 여전히 아이템 위에 있으면 버튼 다시 표시
        if (isEnterMouse)
        {
            activeButton.SetActive(true);
            DeleteButton.SetActive(true);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {    
        Debug.Log("OnDrag");
        if (!isDragging) return;

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos2;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePos, mainCam, out worldPos2);
        rectTransform.position = worldPos2;

        //  드래그 중에도 계속 비활성화 유지
        activeButton.SetActive(false);
        DeleteButton.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마우스가 들어옴!");  
        
        activeButton.SetActive(true);
        DeleteButton.SetActive(true);
        
        SetRaycast(activeButton, false);//아이템 위에 사용,삭제 버튼을 달아두었는데 이 버튼이 아이템의 레이캐스팅 방해해서 레이캐스팅 비활성화
        SetRaycast(DeleteButton, false);

        //아이템 설명창 표시 (ScriptableObject 기반)
        if (itemData != null)
        {
            ExplainItemToText.Instance.ShowDescription(itemData.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("마우스가 나감!");        
        activeButton.SetActive(false);
        DeleteButton.SetActive(false);

        ExplainItemToText.Instance.HideDescription(); //마우스 나가면 설명 숨김
    }
    private void SetRaycast(GameObject obj, bool enable)
    {
        var cg = obj.GetComponent<CanvasGroup>();
        if (cg == null) cg = obj.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = enable;
    }   

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        //  드래그용 Canvas 생성 (임시)
        dragCanvas = gameObject.AddComponent<Canvas>();
        dragCanvas.overrideSorting = true;
        dragCanvas.sortingOrder = 1001;

        // Raycaster도 추가해야 클릭 감지 유지됨
        if (gameObject.GetComponent<GraphicRaycaster>() == null)
            gameObject.AddComponent<GraphicRaycaster>();

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // GraphicRaycaster도 제거 (원래 없던 경우만)
        var raycaster = GetComponent<GraphicRaycaster>();
        if (raycaster != null)
            Destroy(raycaster);

        //  드래그 종료 후 임시 Canvas 제거
        if (dragCanvas != null)
            Destroy(dragCanvas);        
    }
}
