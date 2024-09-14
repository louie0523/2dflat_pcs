using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text findChestText;  // UI에 표시할 텍스트
    public Image clearImage;    // "Clear" 이미지를 표시할 UI 이미지
    private int findChest;
    public AudioClip ClearBgm; // 클리어 시 재생할 사운드
    private AudioSource audioSource; // 오디오 소스 컴포넌트

    private void Awake()
    {
        // 싱글톤 패턴을 통해 어디서든 이 인스턴스에 접근 가능하게 함
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("ScoreManager Instance Initialized: " + (Instance != null));

        // AudioSource 컴포넌트를 게임 오브젝트에 추가
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // FindChest 값을 1 증가
    public void IncreaseFindChest()
    {
        findChest++;
        UpdateFindChestUI();

        // 점수가 3이 되면 게임을 멈추고 "clear" 이미지 표시
        if (findChest >= 3)
        {
            EndGame();
        }
    }

    // FindChest 값을 UI에 업데이트
    private void UpdateFindChestUI()
    {
        if (findChestText != null)
        {
            findChestText.text = findChest.ToString() + "/3";
        }
        else
        {
            Debug.LogWarning("findChestText is not assigned!");
        }
    }

    // 게임을 종료하고 "clear" 이미지를 표시하는 메서드
    private void EndGame()
    {
        Time.timeScale = 0; // 게임 멈추기
        if (clearImage != null)
        {
            clearImage.gameObject.SetActive(true); // "clear" 이미지 표시
        }
        else
        {
            Debug.LogWarning("clearImage is not assigned!");
        }

        // BGM을 멈추고 새로운 BGM을 재생
        if (ClearBgm != null)
        {
            audioSource.clip = ClearBgm;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("ClearBgm is not assigned!");
        }
    }
}
