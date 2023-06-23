using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusNavMesh : MonoBehaviour
{
    [SerializeField] private Transform destinationTransform;
    private NavMeshAgent navMeshAgent;

    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update(){
        navMeshAgent.destination = destinationTransform.position;
    }
}
