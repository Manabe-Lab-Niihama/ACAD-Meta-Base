using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform PlayerTransform;

    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.Find("XR Origin Hands (XR Rig) DemonVersion");

        if (player != null)
        {
            PlayerTransform = player.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(PlayerTransform.position);

    }
}
