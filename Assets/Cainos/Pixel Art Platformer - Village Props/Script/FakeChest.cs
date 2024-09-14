using UnityEngine;
using Cainos.LucidEditor;
using System.Collections; // IEnumerator를 사용하기 위한 네임스페이스

namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class FakeChest : MonoBehaviour
    {
        [FoldoutGroup("Reference")]
        public Animator animator;
        public Transform player; // 플레이어의 Transform을 참조하기 위한 변수
        public GameObject spike; // 'spike' 오브젝트를 참조하기 위한 변수
        public Transform spikePoint; // 'spike'가 이동할 위치를 지정하는 Transform 변수

        [FoldoutGroup("Settings")]
        public float openDistance = 2.0f; // 상자가 열리는 플레이어와의 최소 거리
        public float resetDelay = 10f;   // 상자를 초기화할 지연 시간 (초)

        [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                animator.SetBool("IsOpened", isOpened);

                if (isOpened)
                {
                    ActivateSpike(); // 상자를 열면 'spike' 오브젝트 활성화
                    StartCoroutine(ResetChestAfterDelay()); // 10초 후 자동으로 초기화
                }
                else
                {
                    DeactivateSpike(); // 상자가 닫히면 'spike' 오브젝트 비활성화
                }
            }
        }
        private bool isOpened;

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform; // Player 태그로 자동으로 플레이어 할당
            }

            // 초기 상태에서 상자가 열려 있지 않으면 spike를 비활성화
            if (!IsOpened)
            {
                DeactivateSpike();
            }
        }

        private void Update()
        {
            // 플레이어와의 거리를 계산
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // 플레이어가 지정된 거리 안에 있을 때 상자가 열림
            if (distanceToPlayer < openDistance && !IsOpened)
            {
                IsOpened = true; // 상자 열기
            }
        }

        private void ActivateSpike()
        {
            if (spike != null)
            {
                spike.SetActive(true); // 'spike' 오브젝트 활성화
            }
        }

        private void DeactivateSpike()
        {
            if (spike != null && spikePoint != null)
            {
                spike.transform.position = spikePoint.position; // 'spike'를 'spike_point' 위치로 이동
                spike.SetActive(false); // 'spike' 오브젝트 비활성화
            }
        }

        // 상자를 자동으로 초기화하는 코루틴
        private IEnumerator ResetChestAfterDelay()
        {
            yield return new WaitForSeconds(resetDelay); // 지연 시간 대기
            ResetChest(); // 상자 초기화
        }

        // 상자를 초기화하는 메서드
        public void ResetChest()
        {
            IsOpened = false; // 상자를 닫기
            // spike는 이미 DeactivateSpike()에서 비활성화됨
        }
    }
}
