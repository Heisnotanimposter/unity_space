using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    // 추적할 대상
    public Transform target;

    // 카메라와 대상 사이의 거리
    public float distance = 10f;

    // 카메라의 높이
    public float height = 5f;

    // 카메라의 위치 보간 속도
    public float positionDamping = 5f;

    // 카메라의 회전 보간 속도
    public float rotationDamping = 10f;

    void LateUpdate()
    {
        // 대상이 없으면 리턴
        if (!target) return;

        // 대상의 중심점을 구함
        Vector3 targetPosition = target.position;

        // 카메라가 이동할 목표 위치를 계산함
        // 대상의 뒤쪽 방향으로 거리만큼 떨어지고, 높이만큼 올림
        Vector3 wantedPosition = transform.position;
        wantedPosition.y = targetPosition.y + height;
        Vector3 direction = (targetPosition - wantedPosition).normalized;
        wantedPosition += direction * distance;

        // 카메라가 회전할 목표 각도를 계산함
        // 대상이 바라보는 방향과 같게 함
        Quaternion wantedRotation = Quaternion.LookRotation(targetPosition - wantedPosition);

        // 현재 카메라의 위치와 회전을 가져옴
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // 현재 위치와 목표 위치 사이를 보간하여 부드럽게 이동시킴
        // positionDamping 값이 클수록 빠르게 따라감
        transform.position = Vector3.Lerp(currentPosition, wantedPosition, positionDamping * Time.deltaTime);

        // 현재 각도와 목표 각도 사이를 보간하여 부드럽게 회전시킴
        // rotationDamping 값이 클수록 빠르게 따라감
        transform.rotation = Quaternion.Lerp(currentRotation, wantedRotation, rotationDamping * Time.deltaTime);
    }
}