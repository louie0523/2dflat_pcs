using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;

namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class Chest : MonoBehaviour
    {
        [FoldoutGroup("Reference")]
        public Animator animator;
        public Transform player; // 플레이어의 Transform을 참조하기 위한 변수
        public AudioClip unlockSound; // 상자 열 때 재생할 사운드
        private AudioSource audioSource; // 오디오 소스 컴포넌트

        [FoldoutGroup("Settings")]
        public float openDistance = 2.0f; // 상자가 열리는 플레이어와의 최소 거리

        [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                animator.SetBool("IsOpened", isOpened);
            }
        }
        private bool isOpened;

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform; // Player 태그로 자동으로 플레이어 할당
            }
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가
        }

        private void Update()
        {
            // 플레이어와의 거리를 계산
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            // 플레이어가 지정된 거리 안에 있을 때 상자가 열림
            if (distanceToPlayer < openDistance && !IsOpened)
            {
                IsOpened = true; // 상자 열기
                ScoreManager.Instance.IncreaseFindChest(); // 점수 증가
                PlayUnlockSound(); // 사운드 재생
            }
        }

        // 사운드를 재생하는 메서드
        private void PlayUnlockSound()
        {
            if (unlockSound != null && !audioSource.isPlaying) // 사운드가 있고, 재생 중이 아닐 때만 재생
            {
                audioSource.clip = unlockSound;
                audioSource.Play();
            }
        }
    }
}