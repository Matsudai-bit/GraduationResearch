using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーコントローラ
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public  float SPEED = 0.0f;     // スピード
    [SerializeField]
    private Transform m_cameraTransform; // カメラのトランスフォーム

    [SerializeField]
    private Animator m_animator;        // アニメーター

    private Vector2 m_moveCommand;  // 移動コマンド
    private Rigidbody m_rb;         // リジッドボディ

    StateMachine<PlayerController> m_stateMachine ;

    private bool m_isRequestAttacking = false;

    public CharacterController m_characterController;



    public Animator Animator => m_animator;
    public Vector2 MoveCommand => m_moveCommand;
    public Rigidbody Rigidbody=> m_rb;
    public Transform CameraTransform => m_cameraTransform;
    public LocalTimeScaleHandle TimeScaleHandler => m_characterController.TimeScaleHandler;

    public bool IsRequestedAttack => m_isRequestAttacking;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_stateMachine = new(this);

        m_stateMachine.PushState<IdlingPlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        m_stateMachine.Update(Time.deltaTime);

        m_isRequestAttacking = false;
    }

    private void FixedUpdate()
    {
        m_stateMachine.FixedUpdate();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();

        m_moveCommand = moveInput;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            m_isRequestAttacking = true;
        }
    }
}
