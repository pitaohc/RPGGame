using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;
    void Start()
    {
    }

    void Awake()
    {
        player = GetComponentInParent<Player>();

    }

    private void CurrentStateTrigger()
    {
        Debug.Log("CurrentStateTrigger");
        player.CallAnimationTrigger();
    }
}
