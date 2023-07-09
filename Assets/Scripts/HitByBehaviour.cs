using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class HitByBehaviour : MonoBehaviour, IDamageableBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] protected HazardForceMultiplierContainer container;
    [SerializeField] private bool useNormalizedForce;
    [SerializeField] private bool destroySourceObject;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PerformBehaviour(DamageData data)
    {
        if (data.sourceName != GetRequiredSourceName())
            return;

        if (GetData().resetVelocity)
            rb.velocity = Vector2.zero;
        
        rb.AddForce((useNormalizedForce ? data.force.normalized : data.force) * GetData().multiplier, ForceMode2D.Impulse);
        
        if (destroySourceObject)
            data.sourceObject.GetComponent<EnvironmentObject>().OnRemove();
    }

    protected abstract string GetRequiredSourceName();
    protected abstract HazardForceData GetData();
}