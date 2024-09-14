using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;
    public Transform respawnPoint; // respawn 위치를 저장할 Transform 변수
    public AudioClip respawnSound; // respawn 사운드
    private AudioSource audioSource;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;  // 스프라이트 방향 전환을 위한 변수
    private bool isGrounded = false;        // 지면에 닿았는지 여부를 판단
    private bool isJumping = false;         // 점프 여부를 확인


    //---------------------------------------------------[Override Function]
    // Initialization
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();  // SpriteRenderer 컴포넌트 가져오기
        audioSource = gameObject.AddComponent<AudioSource>();  // AudioSource 컴포넌트 추가
    }

    // Graphic & Input Updates
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        // y 좌표가 -60 이하로 떨어지면 respawn 위치로 이동
        if (transform.position.y < -60)
        {
            Respawn();
        }
    }

    // Physics engine Updates
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    //---------------------------------------------------[Movement Function]

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;  // 왼쪽으로 이동 시 스프라이트 뒤집기
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;  // 오른쪽으로 이동 시 스프라이트 원상복구
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        // Prevent Velocity amplification.
        rigid.velocity = new Vector2(rigid.velocity.x, 0);  // 기존의 Y 방향 속도를 제거

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
        isGrounded = false;  // 점프 후에는 지면에서 떨어져 있는 상태로 변경
    }

    // 지면에 닿았는지 확인 (Ground 태그 확인)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // Ground 태그와의 충돌 확인
        {
            isGrounded = true;  // 지면에 닿으면 점프 가능
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;  // 지면에서 떨어지면 점프 불가
        }
    }

    // 플레이어를 respawn 위치로 이동시키고 사운드 재생
    void Respawn()
    {
        transform.position = respawnPoint.position;  // respawn 위치로 이동
        audioSource.PlayOneShot(respawnSound);       // respawn 사운드 재생

    }
}
