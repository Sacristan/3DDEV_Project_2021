using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMonsterAnimCatcher : MonoBehaviour
{
    ForestMonster _monster;

    private void Start()
    {
        _monster = GetComponentInParent<ForestMonster>();
    }

    public void OnHitDamage()
    {
        Debug.Log("Sent Hit damage");
        _monster.SendDamageToPlayer();
    }
}
