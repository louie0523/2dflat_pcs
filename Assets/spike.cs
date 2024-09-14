using System.Collections;
using UnityEngine;

public class SpikeHandler : MonoBehaviour
{
    public AudioClip hitSound; // 피격 사운드
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    public float teleportY = -61f; // 텔레포트할 Y 좌표

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandlePlayerHit(collision.gameObject)); // 플레이어가 피격된 상태 처리
        }
    }

    private IEnumerator HandlePlayerHit(GameObject player)
    {
        // 플레이어의 스프라이트 렌더러 가져오기
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null)
        {
            // 플레이어 스프라이트를 잠깐 붉은 색으로 변경
            playerSpriteRenderer.color = Color.red;

            // 피격 사운드 재생
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // 0.1초 대기
            yield return new WaitForSeconds(0.1f);

            // 플레이어의 색상 복원
            playerSpriteRenderer.color = Color.white;

            // 플레이어를 Y = -61로 텔레포트
            player.transform.position = new Vector3(player.transform.position.x, teleportY, player.transform.position.z);
        }
    }
}
