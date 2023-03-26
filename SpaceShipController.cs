using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float acceleration = 1f;  // 가속도
    public float maxSpeed = 50f;  // 최대 속도
    public float rotationSpeed = 50f;  // 회전 속도

    private Vector3 velocity;  // 우주선의 속도 벡터

    void Update()
    {
        // 좌우 회전
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);

        // 가속
        float vertical = Input.GetAxis("Vertical");
        velocity += transform.forward * vertical * acceleration * Time.deltaTime;

        // 최대 속도 제한
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // 우주선 이동
        transform.position += velocity * Time.deltaTime;
    }
}