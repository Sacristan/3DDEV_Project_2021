using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMonster : MonoBehaviour
{
    Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        StartCoroutine(AnimationTestRoutine());
    }

    IEnumerator AnimationTestRoutine()
    {
        //IDLE STATE
        yield return new WaitForSeconds(3f); 
        _animator.SetBool("IsWalking", true);// WALK STATE
        yield return new WaitForSeconds(3f); 
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsAttacking", true);//ATTACK STATE
        yield return new WaitForSeconds(Random.Range(5f, 6f)); 
        _animator.SetTrigger("Died");
    }
}
