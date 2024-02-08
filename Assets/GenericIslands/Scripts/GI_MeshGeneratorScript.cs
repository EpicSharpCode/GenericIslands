using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericIslands
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class GI_MeshGeneratorScript : MonoBehaviour
    {
        [Header("Generate Settings")]
        [SerializeField] float scale = 0.02f;
        [SerializeField] float xOffset = 0;
        [SerializeField] float zOffset = 0;

        [Header("Mesh Settings")]
        [SerializeField] int xSize = 50;
        [SerializeField] int zSize = 50;
        [SerializeField] int maxHeight = 25;


        Mesh generatedMesh;
        GI_GenerateScript.GeneratedParams generatedParams;

        Vector3[] vertexArray;
        int[] trianglesArray;

        // Start is called before the first frame update
        void Start()
        {
            generatedMesh = new Mesh();
            GetComponent<MeshFilter>().mesh = generatedMesh;
            GetComponent<MeshCollider>().sharedMesh = generatedMesh;

            CreateMeshTerrain();
            UpdateMesh();
        }

        public void Update()
        {
            var currentParams = new GI_GenerateScript.GeneratedParams(xSize, zSize, scale, xOffset, zOffset, maxHeight);

            if (!generatedParams.Equals(currentParams))
            {
                CreateMeshTerrain();
                UpdateMesh();
            }
        }

        void CreateMeshTerrain()
        {
            var heightMap = GI_GenerateScript.generateHeightMap(xSize + 1,zSize + 1, scale, xOffset, zOffset, maxHeight);
            vertexArray = new Vector3[(xSize + 1) * (zSize + 1)];

            int currentElement = 0;
            for (int z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    vertexArray[currentElement] = new Vector3(x, heightMap[x, z], z);
                    currentElement++;
                }
            }

            trianglesArray = new int[xSize * zSize * 6];

            int v = 0; int t = 0; //vetex and triangle
            for (int x = 0; x < xSize; x++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    trianglesArray[t + 0] = v + 0;
                    trianglesArray[t + 1] = v + xSize + 1;
                    trianglesArray[t + 2] = v + 1;
                    trianglesArray[t + 3] = v + 1;
                    trianglesArray[t + 4] = v + xSize + 1;
                    trianglesArray[t + 5] = v + xSize + 2;

                    v++;
                    t += 6;
                }
                v++;
            }

            generatedParams = new GI_GenerateScript.GeneratedParams(xSize, zSize, scale, xOffset, zOffset, maxHeight);
        }

        void UpdateMesh()
        {
            generatedMesh.Clear();

            generatedMesh.vertices = vertexArray;
            generatedMesh.triangles = trianglesArray;

            generatedMesh.RecalculateNormals();
        }

        public void AddOffset(float x, float z)
        {
            xOffset += x;
            zOffset += z;
        }

        private void OnDrawGizmos()
        {
            if(vertexArray == null) return;

            Gizmos.color = Color.red;
            foreach(var vertex in vertexArray)
            {
                Gizmos.DrawSphere(vertex, .1f);
            }
        }
    }
}
