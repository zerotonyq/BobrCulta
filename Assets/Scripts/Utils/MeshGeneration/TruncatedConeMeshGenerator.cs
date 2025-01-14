using System.Collections.Generic;
using NUnit.Framework;
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
            var bottom = GenerateVerticesOnCircle(Vector3.zero, Vector3.up, Vector3.right, radiusBottom, circleVerticesCount);

            for (var i = 0; i < bottom.Count; i++) bottom[i] -= height / 2 * Vector3.up;
            for (var i = 0; i < top.Count; i++) top[i] += height / 2 * Vector3.up;

            var trianglesBottom = GenerateCircleTriangles(bottom, false, top.Count);
            var trianglesTop = GenerateCircleTriangles(top, true, 0);

            vertices.AddRange(top);
            vertices.AddRange(bottom);


            var trianglesSide = GenerateSideTriangles(vertices);

            triangles.AddRange(trianglesBottom);
            triangles.AddRange(trianglesTop);
            triangles.AddRange(trianglesSide);

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            return mesh;
        }

        private static List<int> GenerateSideTriangles(List<Vector3> vertices)
        {
            var triangles = new List<int>();

            for (var i = 0; i < vertices.Count / 2 - 2; ++i)
            {
                triangles.Add(i + 1);
                triangles.Add(i + 1 + vertices.Count / 2);
                triangles.Add(i + 2);

                triangles.Add(i + 2);
                triangles.Add(i + 1 + vertices.Count / 2);
                triangles.Add(i + 1 + vertices.Count / 2 + 1);
            }

            triangles.Add(vertices.Count / 2 - 1);
            triangles.Add(vertices.Count - 1);
            triangles.Add(1);

            triangles.Add(1);
            triangles.Add(vertices.Count - 1);
            triangles.Add(vertices.Count / 2 + 1);

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