using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.MeshGeneration
{
    public class NormalsDebugComponent : MonoBehaviour
    {
        private List<Vector3> normals = new();
        private List<Vector3> positions = new();
        public void OnEnable()
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                positions.Add(mesh.vertices[i]);
            }
            
            normals = mesh.normals.ToList();
            Debug.Log(normals.Count);
            Debug.Log(positions.Count);

        }

        private void OnDrawGizmos()
        {
            for (var i = 0; i < normals.Count; i++)
            {
                Gizmos.DrawLine(positions[i], normals[i]);
            }
        }
    }
}