using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동 속도
    [SerializeField]
    float moveSpeed = 3;

    // 회전 속도
    [SerializeField]
    float spinSpeed = 270f;

    // 반동 높이
    [SerializeField]
    float recoilPosY = 0.25f;

    // 반동 속도
    [SerializeField]
    float recoilSpeed = 1.5f;

    // 이동 방향
    Vector3 dir = new Vector3();

    // 회전 방향
    Vector3 rotDir = new Vector3();

    // 이동 가능 여부
    bool canMove = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (canMove)
            {
                // 이동 방향 설정
                dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

                // 회전 방향 설정
                rotDir.Set(-dir.z, 0f, -dir.x);

                // 이동과 회전 코루틴 실행
                StartCoroutine(MoveCo());
                StartCoroutine(SpinCo());

                // 반동 코루틴 실행
                StartCoroutine(RecoilCo());
            }
        }
    }

    IEnumerator MoveCo()
    {
        canMove = false; // 이동 중에는 다른 입력 무시

        // 목표 위치 계산
        Vector3 destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // 목표 위치에 도달할 때까지 이동
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destPos; // 정확한 위치로 보정
        canMove = true; // 이동 가능 상태로 복귀
    }

    IEnumerator SpinCo()
    {
        // 목표 각도 계산
        Quaternion destRot = Quaternion.Euler(rotDir * 90f);

        // 목표 각도에 도달할 때까지 회전
        while (Quaternion.Angle(transform.rotation, destRot) > 0.5f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = destRot; // 정확한 각도로 보정
    }

    IEnumerator RecoilCo()
    {
        // 올라감
        while (transform.position.y < recoilPosY)
        {
            transform.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        // 내려감
        while (transform.position.y > 0)
        {
            transform.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        transform.localPosition = new Vector3(0, 0, 0); // 정확한 위치로 보정
    }
}