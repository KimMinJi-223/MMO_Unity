using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other) // �������� ������� ���� �ȿ� ����?
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
        // �̰Ŵ� ������ �ƴ϶� ���� ����Ѵ�.
        //if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, 10))
        //{ // �̰Ŵ� ���⸸ �ָ� ������ ����., �ٵ� �ڿ� ���� 10�� �ָ� 10m�� ����.
        //    Debug.Log("RayCast");
        //}

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out hit, 10))
        //{ // �̰Ŵ� ���⸸ �ָ� ������ ����., �ٵ� �ڿ� ���� 10�� �ָ� 10m�� ����.
        //    Debug.Log($"RayCast{hit.collider.gameObject.name}");
        //}

        //Vector3 look = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
        //{ // �̰Ŵ� ���⸸ �ָ� ������ ����., �ٵ� �ڿ� ���� 10�� �ָ� 10m�� ����.
        //    Debug.Log($"RayCast{hit.collider.gameObject.name}");
        //}

        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

        //foreach(RaycastHit hit in hits)
        //{
        //    Debug.Log($"RayCast {hit.collider.gameObject.name}");
        //}

        // Debug.Log(Input.mousePosition); // ���� ���콺 ��ǥ�� �ȼ���ǥ�� �����´�.(��ũ�� ��ǥ��)
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // 0 ~ 1���� ���� ��ȯ, ������ �˷��ش�. �̰� ����Ʈ

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position; // ī�޶� ��ġ���� ��ǥ���� ���⺤��
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f , Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera {hit.collider.gameObject.name}");
        //    }
        //}
        // �Ʒ� ó�� �ٲٱ� ����
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
            // mainCamera��� ��ũ�� �̿��ؼ� ã�´�.

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = (1 << 8); // 8��° ��Ʈ�� Ű�� ��
            //int mask = (1 << 8) | (1 << 9); // 8, 9�� Ų��. // 768�̶� �Ȱ���. ���α׷��� ���⸦ �Ẹ��
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            // Raycast ���������ڸ� int�ε� �� �ǳ�?
            // �� ó���� �ǰ� �ִ�.LayerMask�� ������ int�ϳ��̴�. �Ͻ������� ��ȯ�� �� �ǰ� �ִ�.
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {

                Debug.Log($"Raycast Camera {hit.collider.gameObject.tag}");

            }
        }

    }
}
