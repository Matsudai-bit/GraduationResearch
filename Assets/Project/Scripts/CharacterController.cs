using UnityEngine;
using UnityEngine.Playables;

public class CharacterController : MonoBehaviour
{
    private Animator            m_animator;
    private ParticleSystem      m_particle;
    private PlayableDirector    m_director;
    [SerializeField]
    private TrailRenderer       m_trailRenderer;

    CharacterSpeed              m_characterSpeed = new();

    private void Awake()
    {
        m_animator      = (!m_animator)     ? GetComponent<Animator>()          : m_animator;
        m_particle      = (!m_particle)     ? GetComponent<ParticleSystem>()    : m_particle;
        m_director      = (!m_director)     ? GetComponent<PlayableDirector>()  : m_director;
        m_trailRenderer = (!m_trailRenderer)? GetComponent<TrailRenderer>()     : m_trailRenderer;
    }

    private void Start()
    {
        m_characterSpeed.onChangeBaseSpeed = UpdateSpeed;

   //     UpdateSpeed(0.1f);

        m_characterSpeed.Initialize();

        m_characterSpeed.onChangeBaseSpeed?.Invoke(0.5f);
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

        if (m_trailRenderer)
        {
            m_trailRenderer.time /= speed;
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
