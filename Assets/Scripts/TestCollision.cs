using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other) // 물리랑은 상관없이 범위 안에 들어갔나?
    {
        Debug.Log($"Trigger {other.gameObject.name}");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 이거는 방향이 아니라 끝을 줘야한다.
        //if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, 10))
        //{ // 이거는 방향만 주면 끝까지 간다., 근데 뒤에 인자 10을 주면 10m만 간다.
        //    Debug.Log("RayCast");
        //}

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out hit, 10))
        //{ // 이거는 방향만 주면 끝까지 간다., 근데 뒤에 인자 10을 주면 10m만 간다.
        //    Debug.Log($"RayCast{hit.collider.gameObject.name}");
        //}

        //Vector3 look = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
        //{ // 이거는 방향만 주면 끝까지 간다., 근데 뒤에 인자 10을 주면 10m만 간다.
        //    Debug.Log($"RayCast{hit.collider.gameObject.name}");
        //}

        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

        //foreach(RaycastHit hit in hits)
        //{
        //    Debug.Log($"RayCast {hit.collider.gameObject.name}");
        //}

        // Debug.Log(Input.mousePosition); // 현재 마우스 좌표를 픽셀좌표로 가져온다.(스크린 좌표다)
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // 0 ~ 1사이 값을 반환, 비율로 알려준다. 이게 뷰포트

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position; // 카메라 위치에서 목표점의 방향벡터
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f , Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera {hit.collider.gameObject.name}");
        //    }
        //}
        // 아래 처럼 바꾸기 가능
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera {hit.collider.gameObject.name}");

        //    }
        //}

        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // mainCamera라는 태크를 이용해서 찾는다.

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = (1 << 8); // 8번째 비트를 키는 것
            //int mask = (1 << 8) | (1 << 9); // 8, 9를 킨다. // 768이랑 똑같다. 프로그래머 계산기를 써보자
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            // Raycast 마지막인자를 int인데 왜 되나?
            // 다 처리가 되고 있다.LayerMask는 실제로 int하나이다. 암시적으로 변환도 다 되고 있다.
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {

                Debug.Log($"Raycast Camera {hit.collider.gameObject.tag}");

            }
        }

    }
}
