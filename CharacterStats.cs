namespace DI_Statistics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class CharacterStats : MonoBehaviour
    {
        public GameObject floatingTextPrefab;
        public Transform floatingTextSpawnPoint;

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

            Instantiate(floatingTextPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity).GetComponent<FloatingText>().Setup(damage.ToString(), floatingTextSpawnPoint.position);
            Debug.Log(transform.name + " takes " + damage + " damage");

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
            Debug.Log(transform.name + " died");
            if (anim != null)
            {
                anim.SetTrigger("Death");
            }
            if (characterIdentifier.ToLower() != "player")
            {
                QuestController.Instance.QuestAction(Quest.Objective.Type.Kill, characterIdentifier, 1);
            }
        }

        float Calculate_MaxHealth()
        {
            return (3 * CON) + (1.5f * STR);
        }
    }
}
