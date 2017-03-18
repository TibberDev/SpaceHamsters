using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PlacePointsInCircle : EditorWindow
{
    float radius = 10f;
    bool doTransform, doEdgeCollider, doFerrTerrain;

    Transform parentItem = null, placeAround = null;
    EdgeCollider2D edgeCollider = null;
    Ferr2D_Path path = null;

    Vector2 origo = new Vector2(0f, 0f);

    [MenuItem("PlaySlide Utils/PlacePointsInACircle")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlacePointsInCircle));
    }

    void OnGUI()
    {
        radius = EditorGUILayout.FloatField("Radius", radius);
        placeAround = (Transform)EditorGUILayout.ObjectField(placeAround, typeof(Transform), true);

        GUILayout.Label("Parent Transform for Points", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        doTransform = EditorGUILayout.Toggle(doTransform, GUILayout.Width(14f));
        parentItem = (Transform)EditorGUILayout.ObjectField(parentItem, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("EdgeCollider GameObject", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        doEdgeCollider = EditorGUILayout.Toggle(doEdgeCollider, GUILayout.Width(14f));
        edgeCollider = (EdgeCollider2D)EditorGUILayout.ObjectField(edgeCollider, typeof(EdgeCollider2D), true);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Ferr2D terrain points", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        doFerrTerrain = EditorGUILayout.Toggle(doFerrTerrain, GUILayout.Width(14f));
        path = (Ferr2D_Path)EditorGUILayout.ObjectField(path, typeof(Ferr2D_Path), true);
        EditorGUILayout.EndHorizontal();


        if (GUI.Button(new Rect(3, 170, position.width - 6, 20), "Make a Circle"))
        {
            if(doTransform && parentItem)
            {
                Transform[] transforms = parentItem.GetComponentsInChildren<Transform>();
                Vector2[] tempPoints = new Vector2[transforms.Length];

                int i = 0;
                foreach (Transform t in transforms)
                {
                    tempPoints[i].x = t.position.x;
                    tempPoints[i].y = t.position.y;

                    i++;
                }

                if (placeAround)
                {
                    PlacePointsInACircle(ref tempPoints, radius, placeAround.position);
                }
                else
                {
                    PlacePointsInACircle(ref tempPoints, radius, Vector2.zero);
                }

                i = 0;
                foreach (Vector2 p in tempPoints)
                {
                    transforms[i].position = new Vector3(p.x, p.y, 0f);
                    i++;
                }
            }
            if (doEdgeCollider && edgeCollider)
            {
                Vector2[] tempPoints = edgeCollider.points;
                
                if (placeAround)
                {
                    PlacePointsInACircle(ref tempPoints, radius, placeAround.position);
                }
                else
                {
                    PlacePointsInACircle(ref tempPoints, radius, Vector2.zero);
                }

                edgeCollider.points = tempPoints;
            }
            if (doFerrTerrain && path)
            {
                Vector2[] tempPoints = path.GetVertsRaw().ToArray();

                if(placeAround)
                {
                    PlacePointsInACircle(ref tempPoints, radius, placeAround.position, false);
                }
                else
                {
                    PlacePointsInACircle(ref tempPoints, radius, Vector2.zero, false);
                }
               
                path.pathVerts = new List<Vector2>(tempPoints);

                path.UpdateColliders();
                path.UpdateDependants(true);
            }
        }   
    }

    public void PlacePointsInACircle(ref Vector2[] points, float radius, Vector2 placeAround, bool isClosed = true)
    {
        int divideNumber = isClosed ? points.Length - 1 : points.Length;
        float angleStep = 360f / divideNumber, currentAngle = 0f;
     
        for(int i = 0; i < points.Length; i++)
        {
            points[i].x = Mathf.Cos(currentAngle / (180f / Mathf.PI)) * radius + placeAround.x;
            points[i].y = -Mathf.Sin(currentAngle / (180f / Mathf.PI)) * radius + placeAround.y;

            currentAngle += angleStep;
        }
    }
}
