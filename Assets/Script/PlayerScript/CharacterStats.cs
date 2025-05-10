using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stats strenght;
    public Stats damange;
    public Stats maxHealth;
    

    [SerializeField] private int currentHealth;

   protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();

        
    }

    public virtual void DoDamange(CharacterStats _targetStats)
    {

        int totalDamage = damange.GetValue() + strenght.GetValue();

        _targetStats.TakeDamage(totalDamage);

    }
    public virtual void TakeDamage(int _damange)
    {
        currentHealth -= _damange;

        Debug.Log(_damange);

        if(currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
      //  throw new NotImplementedException();
    }
}
