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

        Debug.Log("歩く状態の開始");
    }

    /// <summary>
    /// 状態のフィクス更新時に呼ばれる
    /// </summary>
    /// <param name="deltaTime">フレーム</param>
    protected override void OnFixedUpdate()
    {
            var rb = Owner.Rigidbody;
        if (!Mathf.Approximately(0.0f, Owner.MoveCommand.sqrMagnitude))
        {
            // 1. 入力ベクトルを3Dのベクトルにする (Input Space)
            Vector3 inputRaw = new Vector3(Owner.MoveCommand.x, 0.0f, Owner.MoveCommand.y);

            // 2. カメラの回転から「Y軸（左右）の回転だけ」を取り出す
            // カメラのForward方向から上方向成分を消して平坦にする
            Vector3 cameraForward = Owner.CameraTransform.forward;
            cameraForward.y = 0;
            Quaternion cameraYawRotation = Quaternion.LookRotation(cameraForward);

            // 3. 入力ベクトルをカメラの向きに合わせて回転させる (World Space)
            Vector3 moveDirection = cameraYawRotation * inputRaw;

            // 4. 移動（加速度計算）
            Vector3 force = moveDirection * Owner.SPEED /** Owner.TimeScaleHandler.CurrentTimeScale*/;
            rb.AddForce(force);

            // 速度制限
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Owner.SPEED /** Owner.TimeScaleHandler.CurrentTimeScale*/);

            // 5. 回転：キャラクターを入力の方向（moveDirection）へ向かせる
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, 10.0f));

        }
        else if (Mathf.Approximately(0.0f, rb.linearVelocity.sqrMagnitude))
        {
            Machine.PopState();
        }
        Debug.Log("速度" + rb.linearVelocity.magnitude);

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
    }

    /// <summary>
    /// 状態終了時に呼ばれる
    /// </summary>
    protected override void OnExitState()
    {
        m_animationEventHandler.StopAnimation();
        Debug.Log("歩く状態の終了");

    }
}
