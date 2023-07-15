using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
  
    [Header("Hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField] Vector2 moveLimits;
    [SerializeField] float radius;
    // [SerializeField] Transform holeCenter;

    [Space]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform holeTransform;
    [SerializeField] float holeWidth;

    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount;
    float holescale;

    public static bool movemnet;

    private bool canMoveHole = false;

    float x,y;
    Vector3 touch,targetPos;

    void Start()
    {
        movemnet = true;

        Game.isMoving = false;
        Game.isGameover = false;

        holeVertices = new List<int> ();
        offsets = new List<Vector3> ();

        mesh = meshFilter.mesh;

        FindHoleVertices ();

        // DELÝÐÝN 1SN BEKLEYÝP ÇALIÞMASINI SAÐLAYAN KOD
        StartCoroutine(EnableMoveHoleAfterDelay(1f));
    }


    void Update()
    {
        holescale = UndergroundCollision.holescale;
        
        Game.isMoving = Input.GetMouseButton (0);

        if(!Game.isGameover && Game.isMoving) 
        {
            MoveHole ();
            UpdateHoleVerticesPosition ();
        }

        // CANMOVEHOLE TRUE ÝSE METOT ÇALIÞIYOR
        if (canMoveHole)
        {
            MoveHole();
        }
    }

    void MoveHole () 
    {
        if (movemnet)
        {
            if (movemnet)
            {
                x = Input.GetAxis("Mouse X");
                y = Input.GetAxis("Mouse Y");

                touch = Vector3.Lerp(holeTransform.position, holeTransform.position + new Vector3(x, 0f, y), moveSpeed * Time.deltaTime);

                targetPos = new Vector3(
                    Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
                    touch.y,
                    Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y));

                holeTransform.position = targetPos;
            }
        }
    }

    // DELÝÐÝN IENUMERATOR METODU
    IEnumerator EnableMoveHoleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMoveHole = true;
    }

    void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;
        Vector3 scale = new Vector3(holescale, holeTransform.localScale.y, holescale);

        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeTransform.position + Vector3.Scale(offsets[i], scale);
        }

        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeTransform.position, mesh.vertices[i]);

            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeTransform.position);
            }
        }
        holeVerticesCount = holeVertices.Count;
    }


    void OnDrawGizmos () 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (holeTransform.position, radius);
    }

}
