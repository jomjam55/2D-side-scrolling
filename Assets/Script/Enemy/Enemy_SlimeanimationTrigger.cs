using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SlimeanimationTrigger : MonoBehaviour
{
    private Enemy_Slime enemy => GetComponentInParent<Enemy_Slime>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();


    }

    private void AttackTrigger()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamange(target);

       
            }
        }
    }
}
