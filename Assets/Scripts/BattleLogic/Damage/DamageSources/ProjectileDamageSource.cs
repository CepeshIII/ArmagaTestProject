using UnityEngine;


public class ProjectileDamageSource : MonoBehaviour, IDamageSource
{
    private CombatPayload payload;
    private ICombatResolver combatResolver;
    private Transform target;
    private ProjectileData projectileData;
    private Animator animator;
    private Vector3 direction;


    public void OnEnable()
    {
        animator = GetComponent<Animator>();
    }


    public void Initialize(
        Vector3 origin,
        Transform target,
        ProjectileData projectileData,
        CombatPayload payload,
        ICombatResolver resolver)
    {
        this.payload = payload;
        this.target = target;
        this.projectileData = projectileData;
        this.combatResolver = resolver;

        transform.position = origin;
        direction = (target.position - transform.position).normalized;
    }


    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        MoveTowardsTarget();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.attachedRigidbody.TryGetComponent(
            out BattleEntity targetEntity))
            return;

        if (targetEntity == payload.Source 
            || targetEntity.Team == payload.Source.Team)
        {
            return;
        }

        payload.Target = targetEntity;
        combatResolver.Resolve(ref payload);

        Destroy(gameObject);
    }


    private void MoveTowardsTarget()
    {
        var delta = projectileData.speed * Time.deltaTime * direction;
        var newPosition = transform.position + delta;

        UpdateAnimation(direction);

        transform.position = newPosition;
    }



    private void UpdateAnimation(Vector3 direction)
    {
        direction = direction.normalized;
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }
}


public class MeleeDamageSource : IDamageSource
{
    public void Initialize(
            Vector3 origin,
            Transform target,
            AttackData attackData,
            CombatPayload payload,
            ICombatResolver resolver
        )
    {
            var rayCast2D = Physics2D.CircleCastAll(
            origin,
            attackData.radius, 
            Vector2.zero, 
            0f, 
            LayerMask.GetMask("Units")
            );

        foreach (var hit in rayCast2D)
        {
            if(hit.rigidbody != null)
            {
                var victim = hit.rigidbody?.GetComponent<BattleEntity>();
                if(victim == null || victim.gameObject == payload.Source)
                       continue;

                Debug.Log(victim + " has been hit");
                resolver.Resolve(ref payload);
            }
        }

    }
}