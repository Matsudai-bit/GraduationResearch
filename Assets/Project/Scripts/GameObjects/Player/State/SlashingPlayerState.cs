using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// プレイヤーの待機状態
/// </summary>
public class SlashingPlayerState : StateBase<PlayerController>
{
    private  AnimationEventHandler m_animationEventHandler; // アニメーションイベントハンドラー

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public SlashingPlayerState()
    {
    }

    /// <summary>
    /// 状態開始時に呼ばれる
    /// </summary>
    protected override void OnStartState()
    {


        m_animationEventHandler = new(Owner.Animator);

       // Owner.Animator.Play("Slashing", m_animationEventHandler.LayerIndex, 0.0f);

        m_animationEventHandler.PlayAnimation("Slashing", "BaseLayer");
        

        Owner.Slash();
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

        // アニメーションイベントハンドラーの更新
        m_animationEventHandler.OnUpdate();
     
        // アニメーションが始まっている場合の処理
        if (m_animationEventHandler.HasAnimationPlayed())
        {

            OnAnimating();
        }

    }

    private void OnAnimating()
    {
        if (Owner.Animator.GetCurrentAnimatorStateInfo(m_animationEventHandler.LayerIndex).normalizedTime > 0.5f 
            && Owner.IsRequestedSlashing)
        {

            m_animationEventHandler.RestartAnimation(); // 再生済みフラグをリセット

            Machine.ChangeState<SlashingPlayerState>();
            Owner.ResetIsSlashing();

            // ここで使いたい
        }
        else if (!m_animationEventHandler.IsPlaying())
        {
            Machine.PopState();
        }
    }

    /// <summary>
    /// 状態終了時に呼ばれる
    /// </summary>
    protected override void OnExitState()
    {
        //m_animationEventHandler.StopAnimation();
        m_animationEventHandler.ResetAnimation();
    }
}
