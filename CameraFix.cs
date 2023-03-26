using UnityEngine;

public class CameraFix : MonoBehaviour
{
    // 추적할 대상
    public Transform target;

    // 카메라와 대상 사이의 거리
    public float distance = 10f;

    // 카메라의 높이
    public float height = 5f;

    // 카메라의 위치 보간 속도
    public float positionDamping = 5f;

    // 카메라의 고정된 회전 각도
    public Vector3 fixedRotation = new Vector3(30f, 0f, 0f);

    void LateUpdate()
    {
        // 대상이 없으면 리턴
        if (!target) return;

        // 대상의 현재 위치를 가져옴
        Vector3 targetPosition = target.position;

        // 카메라가 이동할 목표 위치를 계산함
        // 대상의 뒤쪽 방향으로 거리만큼 떨어지고, 높이만큼 올림
        Vector3 wantedPosition = targetPosition - Vector3.forward * distance + Vector3.up * height;

        // 현재 카메라의 위치를 가져옴
        Vector3 currentPosition = transform.position;

        // 현재 위치와 목표 위치 사이를 보간하여 부드럽게 이동시킴
        // positionDamping 값이 클수록 빠르게 따라감
        transform.position = Vector3.Lerp(currentPosition, wantedPosition, positionDamping * Time.deltaTime);

        // 카메라의 회전을 고정된 값으로 설정함
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}