using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laserBeam;
    [SerializeField] Collider2D laserCollider;


    Vector3 center;
    Vector3 size;
    Vector2 offset;

    private Vector2 direction;
    private float lastDist;

    // Start is called before the first frame update
    void Start()
    {
        center = gameObject.transform.position;
        //Get direction
        int zRotation = (int)gameObject.transform.rotation.eulerAngles.z % 360;
        switch (zRotation) {
            case 0:
                size = new Vector2(0.75f, 1f);
                offset = new Vector2(0f, 0.5f);
                direction = Vector2.up;
                break;
            case 90:
                size = new Vector2(1f, 0.75f);
                offset = new Vector2(0.5f, 0f);
                direction = Vector2.left;
                break;
            case 180:
                size = new Vector2(0.75f, 1f);
                offset = new Vector2(0f, 0.5f);
                direction = Vector2.down;
                break;
            case 270:
                size = new Vector2(1f, 0.75f);

                offset = new Vector2(0.5f, 0f);
                direction = Vector2.right;
                break;
        }

        //Create Laser
        CreateLaser();
    }

    // Update is called once per frame
    void Update()
    {
        CreateLaser();
    }

    private void CreateLaser () {

        //Choose position of laser depending on what's in front
        int layer = 1 << 9 | 1 << 10;
        RaycastHit2D hit = BoxCast(center, size, layer);
        if (hit.distance != lastDist) {
            if (hit.collider != null) {
                laserBeam.transform.position = (Vector2)center + offset + direction * hit.distance /2f;
                laserBeam.transform.localScale = new Vector2(1f, hit.distance);
            }
        }
    }
    RaycastHit2D BoxCast(Vector2 origin, Vector2 size, int mask) {
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, direction, 100f, mask);

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

        //p1 += origin;
        //p2 += origin;
        //p3 += origin;
        //p4 += origin;

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
