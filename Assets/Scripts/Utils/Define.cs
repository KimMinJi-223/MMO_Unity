using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game
    }
    
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    public enum UIEvent
    {
        Click,
        Drag
    }
    public enum MouseEvent
    {
        Press,
        PointerDown, // �� ó�� ��������
        PointerUp, // �÷����� // �̰� �ΰ��� �� �ʵ��� �����ٰ� ���� �׷��ŷ� Ŭ���̶��� �ǹ̰� �ٸ�
        Click, //

    }

    public enum CameraMode
    {
        QuarterView 
    }
}
