using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // z축 방향으로 0.1도씩 회전
        transform.Rotate(0f, 0f, 0.1f);
    }
}