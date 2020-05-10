using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.CustomLevelEditor
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(MeshRenderer))]
    public class NodeInterface : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Vector3 center = GetComponent<Renderer>().bounds.center;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, new Vector3(1, 1, 1));
        }
    }
}