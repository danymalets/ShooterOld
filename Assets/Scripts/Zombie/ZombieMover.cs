using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DanyExtensions;
using Random = UnityEngine.Random;

public class ZombieMover : MonoBehaviour
{
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _safeDistance = 5f;
    [SerializeField] private float _attackAngle = 10f;
    [SerializeField] private float _angularSpeed = 120;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Coroutine _following;
    private Player _target;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public void SetTarget(Player target)
    {
        _target = target;
    }

    public void StartFollow()
    {
        _animator.Play(ZombieAnimator.Animations.Walk, -1, Random.value);
        _following = StartCoroutine(Follow());
    }

    private IEnumerator Follow()
    {
        while (true)
        {
            if (Vector3.SqrMagnitude(transform.position - _target.Position) < _attackDistance * _attackDistance)
            {
                _animator.SetBool(ZombieAnimator.Parameters.Punch, transform.AngleTo(_target.Position) < _attackAngle);
                transform.RotateTowards(_target.Position, _angularSpeed * Time.deltaTime);
                _navMeshAgent.ResetPath();
                yield return null;
            }
            else
            {
                _animator.SetBool(ZombieAnimator.Parameters.Punch, false);
                _navMeshAgent.SetDestination(_target.Position);
                if (Vector3.SqrMagnitude(transform.position - _target.Position) > _safeDistance * _safeDistance)
                {
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }

    public void Punch()
    {
        Debug.Log("Punch");
    }

    public void StopFollowing()
    {
        StopCoroutine(_following);
        _navMeshAgent.ResetPath();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _safeDistance);
    }
}
