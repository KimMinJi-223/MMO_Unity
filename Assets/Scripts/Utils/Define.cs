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
        PointerDown, // 맨 처을 눌렀을때
        PointerUp, // 올렸을때 // 이거 두개는 몇 초동안 눌렀다가 떼는 그런거로 클릭이랑은 의미가 다름
        Click, //

    }

    public enum CameraMode
    {
        QuarterView 
    }
}
