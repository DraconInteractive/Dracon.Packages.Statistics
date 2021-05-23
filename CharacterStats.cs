namespace DI_Statistics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class CharacterStats : MonoBehaviour
    {
        public string characterIdentifier;

        public float maxHealth;
        public float currentHealth { get; private set; }

        public float maxMana;
        public float currentMana { get; private set; }

        public float maxStamina;
        public float currentStamina { get; private set; }

        public float STR, DEX, WIS, INT, CON, LUCK, WILL;

        public float damage;
        public float armour;

        public float awareness, stealthDetection, viewDistance;

        [HideInInspector]
        public Animator anim;

        public delegate void OnTakeDamage (float amount);
        public OnTakeDamage onTakeDamage;

        public delegate void OnDeath(string characterID);
        public OnDeath onDeath;

        private void Awake()
        {
            maxHealth = Calculate_MaxHealth();
            currentHealth = maxHealth;
        }

        private void Start()
        {
            VStart();
        }

        public virtual void VStart()
        {

        }

        private void Update()
        {
            VUpdate();
        }

        public virtual void VUpdate()
        {

        }

        public virtual void TakeDamage(float damage)
        {
            damage -= armour;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            currentHealth -= damage;

            if (onTakeDamage != null)
            {
                onTakeDamage?.Invoke(damage);
            }

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                if (anim != null)
                {
                    anim.SetTrigger("Damaged");
                }
                else
                {
                    print("No damage animator on " + gameObject.name);
                }
            }
        }

        public virtual void Die()
        {
            if (anim != null)
            {
                anim.SetTrigger("Death");
            }

            if (onDeath != null)
            {
                onDeath(characterIdentifier);
            }
        }

        float Calculate_MaxHealth()
        {
            return (3 * CON) + (1.5f * STR);
        }
    }
}
