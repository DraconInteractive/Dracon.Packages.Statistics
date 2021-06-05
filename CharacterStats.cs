namespace DI_Statistics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class CharacterStats : MonoBehaviour
    {
        public string characterIdentifier;

        public int level;

        public float MaxHealth
        {
            get
            {
                return ((3 * CON) + (1.5f * STR)) * (1.1f * level);
            }
        }
        public float currentHealth { get; private set; }

        public float MaxMana
        {
            get
            {
                return ((1.5f * WIS) + (1.5f * INT)) * (1.1f * level);

            }
        }

        public float currentMana { get; private set; }

        public float MaxStamina
        {
            get
            {
                return ((1.5f * STR) + (1.5f * DEX)) * (1.1f * level);
            }
        }

        public float currentStamina { get; private set; }

        public float MaxResource
        {
            get
            {
                return ((1.5f * STR) + (1.5f * WIS)) * (1.1f * level);
            }
        }

        public float currentResource { get; private set; }

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

        protected virtual void Awake()
        {
            if (level == 0)
            {
                level = 1;
            }
            currentHealth = MaxHealth;
            currentMana = MaxMana;
            currentStamina = MaxStamina;
            currentResource = MaxResource;
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
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

        public virtual void BuffHealth (float amount)
        {
            currentHealth += amount;
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
    }
}
