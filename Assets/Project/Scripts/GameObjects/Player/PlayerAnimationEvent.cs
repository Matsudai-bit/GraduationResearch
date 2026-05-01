
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("コライダーをオンにする際の処理の登録")]
    [SerializeField]
    public UnityEvent m_onColliderAttack;


    [Header("コライダーをオフにする際の処理の登録")]
    [SerializeField]
    public UnityEvent m_offColliderAttack;

    public void OnColliderAttack()
    {
        m_onColliderAttack?.Invoke();
    }   
    public void OffColliderAttack()
    {
        m_offColliderAttack?.Invoke();
    }
}
