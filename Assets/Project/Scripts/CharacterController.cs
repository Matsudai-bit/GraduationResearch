using UnityEngine;
using UnityEngine.Playables;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Animator            m_animator;
    private ParticleSystem      m_particle;
    private PlayableDirector    m_director;
    [SerializeField]
    private TrailRenderer       m_trailRenderer;
    private Rigidbody m_rb;

    private float m_masterTrailRendererTime = 0.0f;

    public float m_initialTimeScale = 1.0f;

    LocalTimeScaleHandle              m_characterSpeed = new();

    private void Awake()
    {
        m_animator      = (!m_animator)     ? GetComponent<Animator>()          : m_animator;
        m_particle      = (!m_particle)     ? GetComponent<ParticleSystem>()    : m_particle;
        m_director      = (!m_director)     ? GetComponent<PlayableDirector>()  : m_director;
        m_trailRenderer = (!m_trailRenderer)? GetComponent<TrailRenderer>()     : m_trailRenderer;
        

    }

    private void Start()
    {
        m_characterSpeed.onChangeAnimationTimeScale = UpdateSpeed;

   //     UpdateSpeed(0.1f);

        m_characterSpeed.Initialize(GetComponent<LocalTimeScaleLayer>());

        m_masterTrailRendererTime = m_trailRenderer.time;
        m_characterSpeed.SetAnimationTimeScale(m_initialTimeScale);

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
            m_trailRenderer.time = m_masterTrailRendererTime  /  speed;
        }

        // Timelineのスピード更新
        if (m_director)
        {
            for (var i = 0; i < m_director.playableGraph.GetRootPlayableCount(); i++)
            {
                m_director.playableGraph.GetRootPlayable(i).SetSpeed(speed);
            }
        }
        Debug.Log("速度の変更私は" + gameObject.name + "\n　所持しているタグの値は "+ GetComponent<LocalTimeScaleLayer>().TimeScaleLayerMask +　"\n　現在の速度は" + speed);

    }
 
    // Update is called once per frame
    void Update()
    {

    }
}
