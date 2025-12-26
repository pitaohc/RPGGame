using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Move after the MonoBehaviour is created
    private Entity entity;
    void Start()
    {
    }

    void Awake()
    {
        entity = GetComponentInParent<Entity>();

    }

    private void CurrentStateTrigger()
    {
        //Debug.Log("CurrentStateTrigger");
        entity.CurrentStateAnimationTrigger();
    }
}
