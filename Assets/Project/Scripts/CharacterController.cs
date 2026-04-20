using UnityEngine;
using UnityEngine.Playables;

public class CharacterController : MonoBehaviour
{
    private Animator m_animator;
    private ParticleSystem m_particle;
    private PlayableDirector m_director;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_particle = GetComponent<ParticleSystem>();
        m_director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        UpdateSpeed(0.1f);
    }

    private void UpdateSpeed(float speed)
    {
        // Animatorのスピード更新
        if (m_animator)
        {
            m_animator.speed = speed;
        }

        // ParticleSystemのスピード更新
        if (m_particle)
        {
            var main = m_particle.main;
            main.simulationSpeed = speed;

        }

        // Timelineのスピード更新
        if (m_director)
        {
            for (var i = 0; i < m_director.playableGraph.GetRootPlayableCount(); i++)
            {
                m_director.playableGraph.GetRootPlayable(i).SetSpeed(speed);
            }
        }

    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
