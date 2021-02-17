using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laserBeamStartPrefab;
    [SerializeField] GameObject laserBeamPrefab;
    private List<GameObject> lasers;

    [SerializeField] Collider2D laserCollider;

    private Vector2 direction;

    [SerializeField] LayerMask worldLayer;
    // Start is called before the first frame update
    void Start()
    {
        lasers = new List<GameObject>();
        //Get direction
        int zRotation = (int)gameObject.transform.rotation.eulerAngles.z % 360;

        print(zRotation);
        switch (zRotation) {
            case 0:
                direction = Vector2.up;
                break;
            case 90:
                direction = Vector2.left;
                break;
            case 180:
                direction = Vector2.down;
                break;
            case 270:
                direction = Vector2.right;
                break;
        }
        print(direction);
        //Create Laser
        CreateLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPaused) {
            Vector3 center;
            Vector3 size;
            center = gameObject.transform.position + (lasers.Count - 1) * (Vector3)direction;
            size = new Vector3(0.75f, 1f);

            print(center);
            RaycastHit2D hit = BoxCast(center, size, worldLayer);
            if (hit.collider == null) {
                CreateLaser();
                print(1);
            } else {
                print(2);
                if (((Vector3)hit.centroid - lasers[lasers.Count - 1].transform.position).magnitude <= 1) {
                    lasers[lasers.Count - 1].transform.position = hit.centroid;
                } else {
                    lasers[lasers.Count - 1].transform.position = gameObject.transform.position + lasers.Count * (Vector3)direction;
                    CreateLaser();
                }
            }
        }

    }

    private void CreateLaser () {
        Vector3 center;
        Vector3 size = new Vector3(0.75f, 1f); ;
        //First laser block
        if (lasers.Count == 0) {
            center = gameObject.transform.position;
        //Later laser blocks
        } else {
            center = lasers[lasers.Count - 1].transform.position;
        }

        //Create laser prefab
        GameObject go = Instantiate(laserBeamPrefab);

        //set child and set proper rotation
        go.transform.SetParent(gameObject.transform);
        go.transform.rotation = gameObject.transform.rotation;
        go.name = "laser" + (lasers.Count);

        //Add to lasers list
        lasers.Add(go);

        //Choose position of laser depending on what's in front
        print(center);
        RaycastHit2D hit = BoxCast(center, size, worldLayer);
        print(hit.distance);
        if (hit.collider != null) {
            go.transform.position = hit.centroid;
        } else {
            go.transform.position = (Vector2)center + direction;
            CreateLaser();
        }
    }
    RaycastHit2D BoxCast(Vector2 origen, Vector2 size, LayerMask mask) {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, 0, direction, 1f, mask);

        ////Setting up the points to draw the cast
        //Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        //float w = size.x * 0.5f;
        //float h = size.y * 0.5f;
        //p1 = new Vector2(-w, h);
        //p2 = new Vector2(w, h);
        //p3 = new Vector2(w, -h);
        //p4 = new Vector2(-w, -h);

        //Quaternion q = Quaternion.AngleAxis(0, new Vector3(0, 0, 1));
        //p1 = q * p1;
        //p2 = q * p2;
        //p3 = q * p3;
        //p4 = q * p4;

        //p1 += origen;
        //p2 += origen;
        //p3 += origen;
        //p4 += origen;

        //Vector2 realDistance = direction.normalized * 1f;
        //p5 = p1 + realDistance;
        //p6 = p2 + realDistance;
        //p7 = p3 + realDistance;
        //p8 = p4 + realDistance;


        ////Drawing the cast
        //Color castColor = hit ? Color.red : Color.green;
        //Debug.DrawLine(p1, p2, castColor, 10000f);
        //Debug.DrawLine(p2, p3, castColor, 10000f);
        //Debug.DrawLine(p3, p4, castColor, 10000f);
        //Debug.DrawLine(p4, p1, castColor, 10000f);

        //Debug.DrawLine(p5, p6, castColor, 10000f);
        //Debug.DrawLine(p6, p7, castColor, 10000f);
        //Debug.DrawLine(p7, p8, castColor, 10000f);
        //Debug.DrawLine(p8, p5, castColor, 10000f);

        //Debug.DrawLine(p1, p5, Color.grey, 10000f);
        //Debug.DrawLine(p2, p6, Color.grey, 10000f);
        //Debug.DrawLine(p3, p7, Color.grey, 10000f);
        //Debug.DrawLine(p4, p8, Color.grey, 10000f);
        //if (hit) {
        //    Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        //}
        return hit;
        }
    }
