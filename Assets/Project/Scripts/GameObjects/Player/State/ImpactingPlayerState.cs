using UnityEngine;

/// <summary>
/// プレイヤーの衝撃状態
/// </summary>
public class ImpactingPlayerState : StateBase<PlayerController>
{
    private AnimationEventHandler m_animationEventHandler; // アニメーションイベントハンドラー

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ImpactingPlayerState()
    {
    }

    /// <summary>
    /// 状態開始時に呼ばれる
    /// </summary>
    protected override void OnStartState()
    {
        m_animationEventHandler = new(Owner.Animator);

        m_animationEventHandler.PlayAnimationBool("Impacting", "BaseLayer", "Impacting");

        //Owner.Animator.SetBool("Running", true)

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

        if (!m_animationEventHandler.IsPlaying())
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
