using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecovery = 1.0f;

    public bool CounterAttackPerformed()
    {
        bool detected = false;
        foreach (Collider2D target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();
            if (counterable == null) continue;

            if (counterable.CanBeCountered)
            {
                detected = true;
                counterable.HandleCounter();
            }
        }
        return detected;
    }

    public float GetCounterRecover()
    {
        return counterRecovery;
    }

}
