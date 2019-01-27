using UnityEngine;
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
                if (GameManager.Instance.playerReference != null)
                {
                    return GameManager.Instance.playerReference.transform.position;
                }
                else
                    return OwnLocation;
            }
        }

        private Vector3 OwnLocation { get { return this.transform.position; } }
        private float seeRange = 7f;
        private float attackRange = 2f;

        // Misc
        [HideInInspector]
        public string Name = "Abstract Enemy";
        public float Health = 100;

        protected virtual void Start()
        {
            GameManager.Instance.EnemiesInGame++;
            agent.stoppingDistance = attackRange;
        }

        protected virtual void Update()
        {
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
                    agent.SetDestination(PlayerLocation);
                    break;
                case Stage.Attack:

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

        // Subtracts health and checks if we died
        public virtual void TakeDamage(float damage)
        {
            Health -= damage;

            if(Health <= 0)
            {
                OnDeath();
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            Gizmos.DrawSphere(this.transform.position, seeRange);

            Gizmos.color = new Color(0,0,1,0.5f);
            Gizmos.DrawSphere(this.transform.position, attackRange);
        }
    }
}