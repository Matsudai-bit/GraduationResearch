using UnityEngine;

/// <summary>
/// プレイヤーの待機状態
/// </summary>
public class IdlingPlayerState : StateBase<PlayerController>
{
    private  AnimationEventHandler m_animationEventHandler; // アニメーションイベントハンドラー

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public IdlingPlayerState()
    {
    }

    /// <summary>
    /// 状態開始時に呼ばれる
    /// </summary>
    protected override void OnStartState()
    {
        m_animationEventHandler = new(Owner.Animator);

        //m_animationEventHandler.PlayAnimationBool("Attack", "BaseLayer", "Attack");
    }

    /// <summary>
    /// 状態のフィクス更新時に呼ばれる
    /// </summary>
    /// <param name="deltaTime">フレーム</param>
    protected override void OnFixedUpdate()
    {
          
    }

    /// <summary>
    /// 状態の更新時に呼ばれる
    /// </summary>
    /// <param name="deltaTime">フレーム</param>
    protected override void OnUpdate(float deltaTime)
    {
        m_animationEventHandler.OnUpdate();

        if (!Mathf.Approximately(0.0f, Owner.MoveCommand.sqrMagnitude))
        {
            Machine.PushState<WalkingPlayerState>();

        }

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
        
    }
}
