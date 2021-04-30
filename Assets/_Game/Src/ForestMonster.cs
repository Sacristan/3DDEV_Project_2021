using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForestMonster : MonoBehaviour
{
    public enum State
    {
        None,
        Idle,
        Following,
        Attacking,
        Dead
    }

    Animator _animator;
    NavMeshAgent _navmeshAgent;
    Player _player;
    State currentState = State.None;
    [SerializeField] float closeEnoughDistance = 2f;
    [SerializeField] float triggerAttackDistance = 10f;

    int health = 100;

    public State CurrentState
    {
        get => currentState;

        private set
        {
            if (currentState != value)
            {
                currentState = value;
                UpdateState();
            }
        }
    }

    void UpdateState()
    {
        if (CurrentState == State.Dead) Stop();
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        _animator.SetBool("IsWalking", CurrentState == State.Following);// WALK STATE
        _animator.SetBool("IsAttacking", CurrentState == State.Attacking);//ATTACK STATE
        if (CurrentState == State.Dead) _animator.SetTrigger("Died");
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        CurrentState = State.Idle;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            CurrentState = State.Following;
            DoDamage(damage: 10);
        }
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case State.Idle:
                if (IsCloseEnough(triggerAttackDistance)) CurrentState = State.Following;
                break;
            case State.Following:
                FollowPlayer();
                break;
            case State.Attacking:
                if (!IsCloseEnough(closeEnoughDistance)) CurrentState = State.Following;
                break;
        }
    }

    void FollowPlayer()
    {
        if (IsCloseEnough(closeEnoughDistance))
        {
            Stop();
            CurrentState = State.Attacking;
        }
        else
        {
            if (_navmeshAgent.isStopped) _navmeshAgent.isStopped = false;
            _navmeshAgent.SetDestination(_player.transform.position);
        }
    }

    private bool IsCloseEnough(float distance) => Vector3.Distance(transform.position, _player.transform.position) <= distance;
    private void Stop()
    {
        if (!_navmeshAgent.isStopped)
        {
            _navmeshAgent.isStopped = true;
            _navmeshAgent.velocity = Vector3.zero;
        }
    }

    public void SendDamageToPlayer()
    {
        _player.ReceiveDamage(20);
    }

    void DoDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, 100);
        if (health <= 0) CurrentState = State.Dead;
    }
}