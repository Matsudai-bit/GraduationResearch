using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameCharacterController m_characterController;
    public Animator m_animator;

    private AnimationEventHandler m_animationEventHandler;

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
    }

    void TakeDamage(int damage, GameObject attacker)
    {
        Debug.Log("Hit!!!!!!!!!!1");
        m_animationEventHandler.PlayAnimationTrigger("Impacting", "BaseLayer", "Impacting");

       transform.LookAt(attacker.gameObject.transform);
    }
}
