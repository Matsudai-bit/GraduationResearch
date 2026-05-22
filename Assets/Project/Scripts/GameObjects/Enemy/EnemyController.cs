using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameCharacterController m_characterController;
    public Animator m_animator;

    private AnimationEventHandler m_animationEventHandler;

    private bool m_isAlive = true;

    public bool IsAlive => m_isAlive;

    private void Awake()
    {
        m_characterController = GetComponent<GameCharacterController>();

        m_animationEventHandler = new(m_animator);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        m_characterController.takeDamage = TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        m_animationEventHandler.OnUpdate();


    }

    void TakeDamage(int damage, GameObject attacker, Vector3 hitPosition)
    {

        if (m_isAlive)
        {
            var effectOption = new EffectPlayOptions();
            effectOption.Speed = m_characterController.TimeScaleHandler.CurrentTimeScale * 0.4f;
            int id = EffectManager.Instance.Play(EffectID.HitEffect, hitPosition, effectOption);


            m_characterController.TimeScaleHandler.onChangeBaseTimeScale += (timeScale) => { EffectManager.Instance.SetSpeed(id, timeScale); };


            m_animationEventHandler.PlayAnimationTrigger("Impacting", "BaseLayer", "Impacting");

            transform.LookAt(attacker.gameObject.transform);

            m_isAlive = false;
            m_animationEventHandler.SetTargetTimeAction(0.5f, () =>
            {
                m_animationEventHandler.ResetTargetTimeAction();

                m_animationEventHandler.PlayAnimationTrigger("Death", "BaseLayer", "Death");
                m_animationEventHandler.SetTargetTimeAction(0.9f, () =>
                {
                    m_animator.speed = 0.0f;
                    m_animationEventHandler.ResetTargetTimeAction();
                });
            });
        }
    }
}
