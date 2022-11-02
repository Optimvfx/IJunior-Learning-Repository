using UnityEngine;

namespace LowPolyWater
{
    public class LowPolyWater : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private UFloat _distanceToRender;

        [SerializeField] private UFloat _waveHeight = 0.5f;
        [SerializeField] private UFloat _waveFrequency = 0.5f;
        [SerializeField] private UFloat _waveLength = 0.75f;

        //Position where the waves originate from
        [SerializeField] private Vector3 _waveOriginPosition = new Vector3(0.0f, 0.0f, 0.0f);

        MeshFilter meshFilter;
        Mesh mesh;
        Vector3[] vertices;

        private void Awake()
        {
            //Get the Mesh Filter of the gameobject
            meshFilter = GetComponent<MeshFilter>();
      
        }

        void Start()
        {
            CreateMeshLowPoly(meshFilter);
        }

        /// <summary>
        /// Rearranges the mesh vertices to create a 'low poly' effect
        /// </summary>
        /// <param name="mf">Mesh filter of gamobject</param>
        /// <returns></returns>
        MeshFilter CreateMeshLowPoly(MeshFilter mf)
        {
            mesh = mf.sharedMesh;

            //Get the original vertices of the gameobject's mesh
            Vector3[] originalVertices = mesh.vertices;

            //Get the list of triangle indices of the gameobject's mesh
            int[] triangles = mesh.triangles;

            //Create a vector array for new vertices 
            Vector3[] vertices = new Vector3[triangles.Length];
            
            //Assign vertices to create triangles out of the mesh
            for (int i = 0; i < triangles.Length; i++)
            {
                vertices[i] = originalVertices[triangles[i]];
                triangles[i] = i;
            }

            //Update the gameobject's mesh with new vertices
            mesh.vertices = vertices;
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            this.vertices = mesh.vertices;

            return mf;
        }
        
        void Update()
        {
            GenerateWaves();
        }

        /// <summary>
        /// Based on the specified wave height and frequency, generate 
        /// wave motion originating from waveOriginPosition
        /// </summary>
        void GenerateWaves()
        {
            var waveOriginTransformOffset = _waveOriginPosition + transform.position;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = vertices[i];

                //Initially set the wave height to 0
                v.y = 0.0f;

                //Get the distance between wave origin position and the current vertex

                float targetDistance = Vector3.Distance(v, _target.position);

                if (targetDistance > _distanceToRender)
                    continue;

                float distance = Vector3.Distance(v, waveOriginTransformOffset);
                distance = (distance % _waveLength) / _waveLength;

                //Oscilate the wave height via sine to create a wave effect
                v.y = _waveHeight * Mathf.Sin(Mathf.PI * 2.0f * _waveFrequency
                + (Mathf.PI * 2.0f * distance));

                //Update the vertex
                vertices[i] = v;
            }

            //Update the mesh properties
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.MarkDynamic();
            meshFilter.mesh = mesh;
        }
    }
}