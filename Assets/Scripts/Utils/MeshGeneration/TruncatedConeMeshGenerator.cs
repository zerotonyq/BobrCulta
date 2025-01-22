using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.MeshGeneration
{
    public static class TruncatedConeMeshGenerator
    {
        public static Mesh Generate(float radiusTop, float radiusBottom, float height, int circleVerticesCount)
        {
            var mesh = new Mesh();

            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            var top = GenerateVerticesOnCircle(Vector3.zero, Vector3.up, Vector3.right, radiusTop, circleVerticesCount);
            var bottom = GenerateVerticesOnCircle(Vector3.zero, Vector3.up, Vector3.right, radiusBottom,
                circleVerticesCount);

            for (var i = 0; i < bottom.Count; i++) bottom[i] -= height / 2 * Vector3.up;
            for (var i = 0; i < top.Count; i++) top[i] += height / 2 * Vector3.up;

            var trianglesBottom = GenerateCircleTriangles(bottom, false, top.Count);
            var trianglesTop = GenerateCircleTriangles(top, true, 0);

            vertices.AddRange(top);
            vertices.AddRange(bottom);

            var trianglesSide = GenerateSideTriangles(vertices);

            triangles.AddRange(trianglesSide);
            triangles.AddRange(trianglesBottom);
            triangles.AddRange(trianglesTop);

            mesh.Clear();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            mesh.Optimize();
            mesh.RecalculateNormals();

            return mesh;
        }

        private static List<int> GenerateSideTriangles(List<Vector3> vertices)
        {
            var triangles = new List<int>();

            var sideVertices = new List<Vector3>();

            for (var i = 1; i < vertices.Count; i++)
            {
                if (i == vertices.Count / 2)
                    continue;

                sideVertices.Add(vertices[i]);
                sideVertices.Add(vertices[i]);
            }

            for (var i = 0; i < sideVertices.Count / 2 - 3; i += 2)
            {
                triangles.Add(i + 1);
                triangles.Add(i + sideVertices.Count / 2);
                triangles.Add(i + 2);
                
                triangles.Add(i + 2);
                triangles.Add(i + sideVertices.Count / 2 );
                triangles.Add(i + sideVertices.Count / 2 +3);
            }

            
            triangles.Add(sideVertices.Count / 2 - 1);
            triangles.Add(sideVertices.Count - 2);
            triangles.Add(0);
            
            
            triangles.Add(0);
            triangles.Add(sideVertices.Count - 2);
            triangles.Add(sideVertices.Count/2 + 1);
            
    
            

            for (var i = 0; i < triangles.Count; i++)
            {
                triangles[i] += vertices.Count;
            }

            vertices.AddRange(sideVertices);

            return triangles;
        }

        private static List<int> GenerateCircleTriangles(List<Vector3> vertices, bool clockwise, int offset)
        {
            var triangles = new List<int>();


            for (var i = 1; i < vertices.Count - 1; i++)
            {
                triangles.Add(0 + offset);
                triangles.Add(i + (!clockwise ? 1 : 0) + offset);
                triangles.Add(i + (clockwise ? 1 : 0) + offset);
            }

            if (clockwise)
            {
                triangles.Add(0 + offset);
                triangles.Add(vertices.Count - 1 + offset);
                triangles.Add(1 + offset);
            }
            else
            {
                triangles.Add(0 + offset);
                triangles.Add(1 + offset);
                triangles.Add(vertices.Count - 1 + offset);
            }

            return triangles;
        }


        private static List<Vector3> GenerateVerticesOnCircle(Vector3 center, Vector3 up, Vector3 right, float radius,
            int count)
        {
            var vertices = new List<Vector3> { center };

            var currentAngle = 0f;
            var deltaAngle = 2 * 180.0f / count;

            var initialVector = Vector3.Cross(up, right) * radius;

            for (var i = 0; i < count; i++)
            {
                currentAngle += deltaAngle;
                var pos = center + Quaternion.AngleAxis(currentAngle, up) * initialVector;
                vertices.Add(pos);
            }

            return vertices;
        }
    }
}