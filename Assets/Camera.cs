using UnityEngine;

public class CameraFollowImmediate : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform을 연결할 변수
    public Vector2 offset;    // 카메라와 플레이어 간의 위치 오프셋

    private void LateUpdate()
    {
        if (player == null)
            return;

        // 카메라의 현재 위치와 플레이어의 목표 위치 계산
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        transform.position = desiredPosition;
    }
}
