using System;
using UnityEngine;

namespace Utils.MeshGeneration
{
    public class Test : MonoBehaviour
    {

        public int segments;
        private void OnValidate()
        {
            var mesh = TruncatedConeMeshGenerator.Generate(3, 3, 2, segments);
            
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }
    }
}