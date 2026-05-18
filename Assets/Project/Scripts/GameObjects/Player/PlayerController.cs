using System.Linq;
using Unity.Android.Gradle;
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

    [Header("ドームオブジェクト")]
    [SerializeField]
    private GameObject m_domeObject;

    [Header("剣のコライダー")]
    [SerializeField]
    private Collider m_sordCollider;

    private Vector2 m_moveCommand;  // 移動コマンド
    private Rigidbody m_rb;         // リジッドボディ

    StateMachine<PlayerController> m_stateMachine ;

    private bool m_isRequestAttacking = false;
    private bool m_isRequestInteracting = false;
    private bool m_isRequestSlashing = false;

    public GameCharacterController m_characterController;



    public Animator Animator => m_animator;
    public Vector2 MoveCommand => m_moveCommand;
    public Rigidbody Rigidbody=> m_rb;
    public Transform CameraTransform => m_cameraTransform;
    public LocalTimeScaleHandle TimeScaleHandler => m_characterController.TimeScaleHandler;

    public bool IsRequestedAttack => m_isRequestAttacking;
    public void ResetIsAttack() { m_isRequestAttacking = false; }
    public bool IsRequestedInteracting => m_isRequestInteracting;
    public void ResetIsInteracting() { m_isRequestInteracting = false; }

    public bool IsRequestedSlashing => m_isRequestSlashing;
    public void ResetIsSlashing() { m_isRequestSlashing = false; }


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_stateMachine = new(this);

        m_stateMachine.PushState<IdlingPlayerState>();
        m_sordCollider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_stateMachine.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        m_stateMachine.FixedUpdate();
    }

    public void Slash()
    {
        var enemies =  m_domeObject.GetComponent<DomeController>().DomeInObjects.FindAll(i => i.tag == "Enemy");
        GameObject nearEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            if (enemy && enemy.GetComponent<EnemyController>() && !enemy.GetComponent<EnemyController>().IsAlive) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearEnemy = enemy;
            }
        }
        if (!nearEnemy) return;

        var enemyDirection =  nearEnemy.transform.position - transform.position;
        var length = enemyDirection.magnitude;
        transform.position = transform.position + enemyDirection.normalized * (length - 1.0f);

        transform.LookAt(nearEnemy.transform);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();

        m_moveCommand = moveInput;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (m_domeObject.activeSelf)
            {
                m_isRequestSlashing = true;
            }
            else
            {
                m_isRequestAttacking = true;
            }
        }

        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
     //   if (!context.performed)
        {
            Debug.Log("osita");
            m_domeObject.SetActive(!m_domeObject.activeSelf);
            m_domeObject.transform.position = new(transform.position.x, 0.0f, transform.position.z);
        }
       
    }

    public void EnableAttack()
    {
        m_sordCollider.gameObject.SetActive(true);
    }
    public void DisableAttack()
    {
        m_sordCollider.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GameCharacterController>())
        {
            other.gameObject.GetComponent<GameCharacterController>().TakeDamage(1, gameObject);
        }
    }
}
