using UnityEngine;

/// <summary>
/// プレイヤーの歩行状態
/// </summary>
public class WalkingPlayerState : StateBase<PlayerController>
{
    private AnimationEventHandler m_animationEventHandler; // アニメーションイベントハンドラー

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public WalkingPlayerState()
    {
    }

    /// <summary>
    /// 状態開始時に呼ばれる
    /// </summary>
    protected override void OnStartState()
    {
        m_animationEventHandler = new(Owner.Animator);

        m_animationEventHandler.PlayAnimationBool("Running", "BaseLayer", "Running");

        //Owner.Animator.SetBool("Running", true)

    }

    [SerializeField] private float m_rotationSpeed = 360.0f; // Inspectorで調整可能に

    protected override void OnFixedUpdate()
    {
        var rb = Owner.Rigidbody;
        if (Owner.MoveCommand.sqrMagnitude > 0.0f)
        {
            Vector3 inputRaw = new Vector3(Owner.MoveCommand.x, 0.0f, Owner.MoveCommand.y);

            Vector3 cameraForward = Owner.CameraTransform.forward;
            cameraForward.y = 0;
            Quaternion cameraYawRotation = Quaternion.LookRotation(cameraForward);
            Vector3 moveDirection = cameraYawRotation * inputRaw;

            float speed = Owner.SPEED * Owner.TimeScaleHandler.CurrentTimeScale;

            // 現在の水平速度
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            // 目標速度との差分だけForceをかける（自然な加減速になる）
            Vector3 targetVelocity = moveDirection * speed;
            Vector3 velocityDiff = targetVelocity - horizontalVelocity;
       
            velocityDiff = Vector3.ClampMagnitude(velocityDiff, Owner.MAX_SPEED);
            rb.AddForce(velocityDiff, ForceMode.VelocityChange);

            // 回転
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, m_rotationSpeed * Time.fixedDeltaTime));


        }
 
    }
    /// <summary>
    /// 状態の更新時に呼ばれる
    /// </summary>
    /// <param name="deltaTime">フレーム</param>
    protected override void OnUpdate(float deltaTime)
    {
       

        m_animationEventHandler.OnUpdate();

        if (Owner.IsRequestedAttack)
        {
            Machine.PushState<AttackingPlayerState>();
        }
        else if (Owner.IsRequestedSlashing)
        {
            Machine.PushState<SlashingPlayerState>();
        }
        else if (Owner.Rigidbody.linearVelocity.sqrMagnitude < (1.0f * 1.0f))
        {
            Machine.PopState();
        }
    }

    /// <summary>
    /// 状態終了時に呼ばれる
    /// </summary>
    protected override void OnExitState()
    {
        m_animationEventHandler.StopAnimation();

    }
}
