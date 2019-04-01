using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [Header("TARGET MANAGER")]

    [Tooltip("Transform do grupo de alvos")]
    [SerializeField] private Transform pathfindingTargets;

    [Tooltip("Seguir alvos aleatórios")]
    [SerializeField] private bool randomIdTarget;

    [Header("VALUES MANAGER")]

    [Range(0, 100)]
    [Tooltip("Velocidade de movimento para o próximo ponto")]
    [SerializeField] private float speedMove;

    [Tooltip("Distância do alvo")] [Range(0,255)]
    [SerializeField] private byte targetDistance;

    [Tooltip("ID do alvo")] [Range(0,255)]
    private byte idTarget;

    private NavMeshAgent navMesh;
    bool caPatrol;

    #region MonoBehaviour
    // Start is called before the first frame update
    private void Start()
    {
        if(this.GetComponent<NavMeshAgent>() != null)
            this.navMesh = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(this.caPatrol)
            this.Patrol();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            this.FollowCharacter(other.transform.position);
        else
            this.caPatrol = true;
    }
    #endregion

    #region Patrol
    /// <summary>
    /// Seguir alvos
    /// </summary>
    void Patrol()
    {
        Vector3 newPositionTarget = this.pathfindingTargets.GetChild(this.idTarget).transform.position;

        if (Vector3.Distance(this.transform.position, newPositionTarget) < this.targetDistance)
        {
            if (!this.randomIdTarget)
            {
                if (this.idTarget < this.pathfindingTargets.childCount - 1)
                    this.idTarget++;
                else
                    this.idTarget = 0;
            }
            else
            {
                int randomIndexTarget = Random.Range(0, this.pathfindingTargets.childCount);
                this.idTarget = (byte) randomIndexTarget;
            }
        }

        if (this.GetComponent<NavMeshAgent>() != null)
            this.navMesh.destination = newPositionTarget;
        else
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPositionTarget, this.speedMove * Time.deltaTime);
    }
    #endregion

    #region FollowCharacter
    /// <summary>
    /// Seguir o jogador
    /// </summary>
    /// <param name="characterPosition">Posição do jogador</param>
    /// <returns>Posição do jogador</returns>
    private Vector3 FollowCharacter(Vector3 characterPosition)
    {
        this.navMesh.destination = characterPosition;
        return characterPosition;       
    }
    #endregion
}