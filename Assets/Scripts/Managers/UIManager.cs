using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class UIManager
{
    int _order = 10; // 최근에 사용한 팝업 오더
    // 팝업 목록을 둬야 한다.
    // 어떤 자료구조가 좋나?
    // 스택이 좋을거다
    // GameObject가 아닌 UI_Popup으로 들고 있는게 훨씬 현명할거란 생각이 든다.
    // 버튼이라는 것을 알 수 있는건 UI컴포넌트가 들고 있다. 그 안에 UI_Popup이 있다.
    // UI_Popup이면 된다. 
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root"); // UI_Root이름을 가지는 오브젝트를 찾는다.
            if (root == null) // 없으면 만든다.
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        // name은 프리팹의 이름
        // T 타입은 스크립트와 관련이 있다. 
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;// 이름이 없으면 타입의 이름을 그대로 사용할 수 있다.

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        // 여기까지 하면 팝업이 만들어짐
    
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        // name은 프리팹의 이름
        // T 타입은 스크립트와 관련이 있다. 
        if (string.IsNullOrEmpty(name)) 
            name = typeof(T).Name;// 이름이 없으면 타입의 이름을 그대로 사용할 수 있다.

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        // 여기까지 하면 팝업이 만들어짐
        // 이제 이 팝업이 가지고 있는 UI_Button 스크립트를 뽑아와야한다.
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform); // 부모님을 지정

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if(_popupStack.Peek() != popup) // 지금 삭제 되어야 할 팝업이 맞는지 확인 (스택 제일 위에 있는건지 본다.)
        {
            Debug.Log("ClosePopup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        // 스택에 있는걸 하나씩 뽑아서 닫는다
        if (_popupStack.Count == 0) // 스택은 항상 카운트 체크를 하자
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null; // destroy하면 이제 이 팝업은 없어진거다. 접근하면 안된다. 실수를 방지하기 위해 null을 넣자
        
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();     
    }
    
    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
