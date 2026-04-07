using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーコントローラ
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float SPEED = 0.0f;     // スピード
    [SerializeField]
    private Transform m_cameraTransform; // カメラのトランスフォーム

    private Vector2 m_moveCommand;  // 移動コマンド
    private Rigidbody m_rb;         // リジッドボディ


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!Mathf.Approximately(0.0f, m_moveCommand.sqrMagnitude))
        {
            // 1. 入力ベクトルを3Dのベクトルにする (Input Space)
            Vector3 inputRaw = new Vector3(m_moveCommand.x, 0.0f, m_moveCommand.y);

            // 2. カメラの回転から「Y軸（左右）の回転だけ」を取り出す
            // カメラのForward方向から上方向成分を消して平坦にする
            Vector3 cameraForward = m_cameraTransform.forward;
            cameraForward.y = 0;
            Quaternion cameraYawRotation = Quaternion.LookRotation(cameraForward);

            // 3. 入力ベクトルをカメラの向きに合わせて回転させる (World Space)
            Vector3 moveDirection = cameraYawRotation * inputRaw;

            // 4. 移動（加速度計算）
            Vector3 force = moveDirection * SPEED;
            m_rb.AddForce(force);

            // 速度制限
            m_rb.linearVelocity = Vector3.ClampMagnitude(m_rb.linearVelocity, SPEED);

            // 5. 回転：キャラクターを入力の方向（moveDirection）へ向かせる
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            m_rb.MoveRotation(Quaternion.RotateTowards(m_rb.rotation, targetRotation, 10.0f)); 

        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();

        m_moveCommand = moveInput;
    }
}
