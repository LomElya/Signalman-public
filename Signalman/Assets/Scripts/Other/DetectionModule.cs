using System;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class DetectionModule : MonoBehaviour
{
    public event Action DetectPlayer;

    [SerializeField] private float _distance;
    [SerializeField] private float _detectionAngle;
    [SerializeField] private float _detectionHeight = 1f;
    [SerializeField] private Color _meshColor = Color.red;
    [SerializeField] private int _scanFrequency = 30;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private LayerMask _occlusionLayers;

    private List<GameObject> _objects = new();

    Collider[] _colliders = new Collider[50];
    private Mesh _mesh;

    private int _count;
    float _scanInterval;
    float _scanTimer;

    private void OnValidate()
    {
        _mesh = CreateWegdeMesh();
        _scanInterval = 1f / _scanFrequency;
    }

    private void Start()
    {
        _scanInterval = 1f / _scanFrequency;
    }

    private void Update()
    {
        _scanTimer -= Time.deltaTime;

        if (_scanTimer < 0)
        {
            _scanTimer += _scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        _count = Physics.OverlapSphereNonAlloc(transform.position, _distance, _colliders, _layers, QueryTriggerInteraction.Collide);

        _objects.Clear();

        for (int i = 0; i < _count; i++)
        {
            GameObject obj = _colliders[i].gameObject;

            if (IsInSight(obj))
            {
                _objects.Add(obj);
                //Debug.Log("Обнаружен");

                DetectPlayer?.Invoke();
            }
        }
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if (direction.y < 0 || direction.y > _detectionHeight)
            return false;

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);

        if (deltaAngle > _detectionAngle)
            return false;

        if (direction.magnitude > _distance)
            return false;

        /*    if (Dot(direction) <= Cos())
               return false; */

        origin.y += _detectionHeight / 2;
        dest.y = origin.y;

        if (Physics.Linecast(origin, dest, _occlusionLayers))
            return false;

        return true;
    }

    private void LateUpdate()
    {
        /*  if (_currentInteracter == null)
             return;

         if (_isSeeingTarget)
             return;

         if (IsDetectPlayer())
         {
             _isSeeingTarget = true;
             DetectPlayer?.Invoke();
         } */
    }

    /*  public bool IsDetectPlayer()
     {
         if (_currentInteracter == null)
             return false;

         Vector3 enemyPosition = transform.position;
         Vector3 toPlayerPosition = _currentInteracter.transform.position - enemyPosition;

         toPlayerPosition.y =0;

         if (toPlayerPosition.magnitude <= _distance)
         {
             if (Dot(toPlayerPosition) > Cos())
                 return true;
         }

         return false;
     } */

    private Mesh CreateWegdeMesh()
    {
        Mesh mesh = new();

        int segments = 10;
        int numTriangls = (segments * 4) + 2 + 2;
        int numVertices = numTriangls * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_detectionAngle, 0) * Vector3.forward * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _detectionAngle, 0) * Vector3.forward * _distance;

        Vector3 topCenter = bottomCenter + Vector3.up * _detectionHeight;
        Vector3 topLeft = bottomLeft + Vector3.up * _detectionHeight;
        Vector3 topRight = bottomRight + Vector3.up * _detectionHeight;

        int vert = 0;

        //Left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //Right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -_detectionAngle;
        float deltaAngle = (_detectionAngle * 2) / segments;

        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _distance;

            topRight = bottomRight + Vector3.up * _detectionHeight;
            topLeft = bottomLeft + Vector3.up * _detectionHeight;

            //Far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //Top side
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //Bottom side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
            triangles[i] = i;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        if (_mesh)
        {
            Gizmos.color = _meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, _distance);

        for (int i = 0; i < _count; i++)
            Gizmos.DrawSphere(_colliders[i].transform.position, 1f);

        Gizmos.color = Color.green;
        foreach (var obj in _objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 1f);
        }
    }
#endif

}
