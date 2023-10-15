using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10; // �ֱٿ� ����� �˾� ����
    // �˾� ����� �־� �Ѵ�.
    // � �ڷᱸ���� ����?
    // ������ �����Ŵ�
    // GameObject�� �ƴ� UI_Popup���� ��� �ִ°� �ξ� �����ҰŶ� ������ ���.
    // ��ư�̶�� ���� �� �� �ִ°� UI������Ʈ�� ��� �ִ�. �� �ȿ� UI_Popup�� �ִ�.
    // UI_Popup�̸� �ȴ�. 
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _scene = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root"); // UI_Root�̸��� ������ ������Ʈ�� ã�´�.
            if (root == null) // ������ �����.
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

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        // name�� �������� �̸�
        // T Ÿ���� ��ũ��Ʈ�� ������ �ִ�. 
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;// �̸��� ������ Ÿ���� �̸��� �״�� ����� �� �ִ�.

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        // ������� �ϸ� �˾��� �������
        // ���� �� �˾��� ������ �ִ� UI_Button ��ũ��Ʈ�� �̾ƿ;��Ѵ�.
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _scene = sceneUI;

        go.transform.SetParent(Root.transform); // �θ���� ����

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        // name�� �������� �̸�
        // T Ÿ���� ��ũ��Ʈ�� ������ �ִ�. 
        if (string.IsNullOrEmpty(name)) 
            name = typeof(T).Name;// �̸��� ������ Ÿ���� �̸��� �״�� ����� �� �ִ�.

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        // ������� �ϸ� �˾��� �������
        // ���� �� �˾��� ������ �ִ� UI_Button ��ũ��Ʈ�� �̾ƿ;��Ѵ�.
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform); // �θ���� ����

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if(_popupStack.Peek() != popup) // ���� ���� �Ǿ�� �� �˾��� �´��� Ȯ�� (���� ���� ���� �ִ°��� ����.)
        {
            Debug.Log("ClosePopup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        // ���ÿ� �ִ°� �ϳ��� �̾Ƽ� �ݴ´�
        if (_popupStack.Count == 0) // ������ �׻� ī��Ʈ üũ�� ����
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null; // destroy�ϸ� ���� �� �˾��� �������Ŵ�. �����ϸ� �ȵȴ�. �Ǽ��� �����ϱ� ���� null�� ����
        
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();     
    }
}
