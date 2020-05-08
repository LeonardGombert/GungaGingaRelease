using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(MeshRenderer))]
    public class NodeInterface : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Vector3 center = GetComponent<Renderer>().bounds.center;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, new Vector3(2, 2, 2));
        }
    }
}