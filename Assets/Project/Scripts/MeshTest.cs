using UnityEngine;

public class MeshTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var mesh = new Mesh();

        var vertices = new[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0)
        };
        mesh.SetVertices(vertices);

        var indices = new[] { 
            0, 1, 2 
        };
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
