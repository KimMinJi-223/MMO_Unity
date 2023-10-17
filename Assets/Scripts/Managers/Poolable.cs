using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    // 이 컴포넌트는 아무 일도 안한다.
    // 그냥 이 컴포넌트를 들고 있으면 메모리 풀링을 한다는 의미이다.
    public bool IsUsing;
}
