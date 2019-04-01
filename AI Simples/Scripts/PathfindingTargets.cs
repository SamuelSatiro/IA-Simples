using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargets : MonoBehaviour
{

    [Tooltip("Ativar o Gizmo")]
    [SerializeField] private bool enableGizmo = true;

    [Tooltip("Transform dos alvos da perseguição")]
    [SerializeField] private Transform contentTargets;

    [Tooltip("Cor do Gizmos")]
    [SerializeField] private Color colorGizmos;

    private void OnDrawGizmos()
    {
        Gizmos.color = this.colorGizmos;
        if (this.contentTargets != null && this.enableGizmo)
        {
            for (int i = 0; i < this.contentTargets.childCount; i++)
            {
               if(i < this.contentTargets.childCount - 1)
                Gizmos.DrawLine(this.contentTargets.GetChild(i).transform.position, 
                    this.contentTargets.GetChild(i+1).transform.position);
            }
        }
    }
}