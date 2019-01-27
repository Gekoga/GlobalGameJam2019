﻿using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public enum Stage
        {
            Idle,
            Walk,
            Attack,
            Dead
        }
        public Stage stage;

        protected NavMeshAgent agent { get { return this.GetComponent<NavMeshAgent>(); } }

        public Vector3 PlayerLocation
        {
            get
            {
                if (Player != null)
                {
                    return Player.transform.position;
                }
                else
                    return OwnLocation;
            }
        }
        public PlayerControllerScript Player
        {
            get
            {
                if (GameManager.Instance.playerReference != null)
                {
                    return GameManager.Instance.playerReference;
                }
                else
                    return null;
            }
        }

        private Vector3 OwnLocation { get { return this.transform.position; } }
        private float seeRange = 7f;
        private float attackRange = 2f;
        private float attackSpeed = 1;
        private float attacktime;
        private bool canAttack
        {
            get
            {
                if (attacktime < Time.time)
                {

                    attacktime = Time.time + attackSpeed;
                    return true;
                }
                else return false;
            }
        }
        public int damage;

        public bool hideGizmos;

        // Misc
        [HideInInspector]
        public string Name = "Abstract Enemy";
        public float Health = 100;
        public int Damage = 20;

        public float PulseRange;
        public float PulseDelay;

        private float t;

        protected virtual void Start()
        {
            GameManager.Instance.EnemiesInGame++;
            if(agent == null)
            {
                return;
            }
            agent.stoppingDistance = attackRange;
        }

        protected virtual void Update()
        {
            if (t < PulseDelay)
            {
                t += Time.deltaTime;
                return;
            }
            DamagePulse();

            float distance = (PlayerLocation - OwnLocation).sqrMagnitude;
            if (distance < seeRange * seeRange)
            {
                if (distance < attackRange * attackRange)
                    stage = Stage.Attack;
                else
                    stage = Stage.Walk;
            }
            else
                stage = Stage.Idle;

            switch (stage)
            {
                case Stage.Idle:
                    break;
                case Stage.Walk:
                    if(agent == null)
                    {
                        break;
                    }
                    agent.SetDestination(PlayerLocation);
                    break;
                case Stage.Attack:
                    Attack();
                    break;
                case Stage.Dead:
                    break;
                default:
                    break;
            }
        }

        public virtual void OnDeath()
        {
            GameManager.Instance.EnemyDeath(this);
        }

        protected virtual void Attack()
        {
            if (canAttack)
                Player.TakeDamage(damage);
            Debug.Log("KNEEEEEEEESSSSS");
        }

        // Subtracts health and checks if we died
        public virtual void TakeDamage(float damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                OnDeath();
            }
        }

        public virtual void DamagePulse()
        {
            RaycastHit hit;
            if (!Physics.SphereCast(transform.position, PulseRange, new Vector3(1, 1, 1), out hit)) return;

            hit.collider.GetComponent<PlayerControllerScript>()?.TakeDamage(Damage);
        }

        public virtual void OnDrawGizmosSelected()
        {
            var alpha = 0.25f;
            Gizmos.color = new Color(1,0,0,alpha);
            Gizmos.DrawSphere(transform.position, PulseRange);
        }

        protected void OnDrawGizmos()
        {
            if (!hideGizmos)
            {
                Gizmos.color = new Color(0, 1, 0, 0.25f);
                Gizmos.DrawSphere(this.transform.position, seeRange);

                Gizmos.color = new Color(0, 0, 1, 0.5f);
                Gizmos.DrawSphere(this.transform.position, attackRange);
            }
        }
    }
}