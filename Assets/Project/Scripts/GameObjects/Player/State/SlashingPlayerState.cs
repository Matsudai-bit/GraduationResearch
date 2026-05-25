using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

/// <summary>
/// プレイヤーの待機状態
/// </summary>
public class SlashingPlayerState : StateBase<PlayerController>
{
    private  AnimationEventHandler m_animationEventHandler; // アニメーションイベントハンドラー

    private float speed;
    private float ratio = 0.0f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool isMoveFinish = false;
    float m_duration;

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


        SetUpSlash();
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
        if (isMoveFinish)
        {

        }

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

    public void SetUpSlash()
    {
        Owner.m_characterController.TimeScaleHandler.SetAnimationTimeScale(1.0f);


        var enemies = Owner.DomeObject.GetComponent<DomeController>().DomeInObjects.FindAll(i => i.tag == "Enemy");
        GameObject nearEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            if (enemy && enemy.GetComponent<EnemyController>() && !enemy.GetComponent<EnemyController>().IsAlive) continue;

            float distance = Vector3.Distance(Owner.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearEnemy = enemy;
            }
        }
        if (!nearEnemy) return;

        var enemyDirection = nearEnemy.transform.position - Owner.transform.position;
        var length = enemyDirection.magnitude;

        var targetLength = length - 1.0f;
        var direction = enemyDirection.normalized;
        var speed =  5.0f / targetLength ;

        ratio = 0.0f;
        //var oneValue = (length-1.0f) / 5.0f;

        //Owner.transform.position = Owner.transform.position + enemyDirection.normalized * (length - 1.0f);

        startPosition = new Vector3(Owner.transform.position.x, Owner.transform.position.y, Owner.transform.position.z);
        endPosition = startPosition +  new Vector3((enemyDirection.normalized * (length - 1.0f)).x, (enemyDirection.normalized * (length - 1.0f)).y, (enemyDirection.normalized * (length - 1.0f)).z);

        Owner.transform.LookAt(nearEnemy.transform);

        isMoveFinish = false;
        m_duration = DebugSettings.Instance.movingTime;
        Owner.OnStartCoroutine(MoveCoroutine());

    }

    public void Slash()
    {
    
    }

    private IEnumerator MoveCoroutine()
    {
        float elapsed = 0f;
        Vector3 startPosition = Owner.transform.position;

        while (elapsed < m_duration)
        {
            elapsed += Time.deltaTime;
            Owner.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / m_duration);
            yield return null;
        }

        // 誤差修正
        Owner.transform.position = endPosition;
        isMoveFinish = true;
    }

}
