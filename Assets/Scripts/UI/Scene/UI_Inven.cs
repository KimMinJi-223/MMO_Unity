using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        
        // 실제 게임에서는 인벤토리 데이터를 통해 여기를 채워준다.
        for(int i = 0; i<8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
         
            //UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item); // 이걸 안하면 아이템 아이콘 프리팹에 스크립트가 안 붙어서 스크립트가 실행되지 않는다. // 근데 에디터에서 붙여줘서 에디터에 붙여준 컴포넌트를 반환함
            // 위에 말고 아래 방식으로 하면 인터페이스가 더 간단하다.
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();

            invenItem.SetInfo($"집행검{i}번");
        }
    }

}
