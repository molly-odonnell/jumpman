using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToMouse : MonoBehaviour {
    // Customization for the looks of the line.
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public float lineWidth = 0.9f;
    public int endVertices = 90;


    // Components of the object.
    public GameObject child = null;
    LineRenderer lineRenderer = null;
    CapsuleCollider2D capsule = null;
    int collisionCount = 0;


    void Start () {

        // Create the lineRenderer or find it.
        if (gameObject.GetComponent<LineRenderer>() != null)
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        else
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set up the lineRenderer.
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.numCapVertices = endVertices;
        // Color Gradient of line.
        Gradient gradient = new Gradient();
        float alpha = 1.0f;
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        // Find the collider (should already exist)
        capsule = gameObject.GetComponent<CapsuleCollider2D>();

    }

    void OnMouseDrag ()
    {
        // Get rid of any wires later in the sequence.
        DestroyRecursive();
        // Endpoints of the segment.
        Vector3 start = transform.position;
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        lineRenderer.SetPosition(1, target);

        // Properly lay out the collider.
        faceMouse(start, target);
        float offsetLength = (start - target).magnitude;
        capsule.offset = new Vector2(0, offsetLength / 2);
        capsule.size = new Vector2(lineWidth, offsetLength + lineWidth);
    }


    void OnMouseUp ()
    {
        Vector3 start = transform.position;
        if (collisionCount == 0)
        {
            // Endpoints on release.
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            lineRenderer.SetPosition(1, target);
            // Create a new wire to extend from.
            target.z -= 0.01f;
            child = Instantiate(gameObject, target, Quaternion.identity);
            child.name = "wire_part";
            child.transform.parent = transform;
            CapsuleCollider2D childCapsule = child.GetComponent<CapsuleCollider2D>();
            childCapsule.offset = Vector2.zero;
            childCapsule.size = new Vector2(lineWidth, lineWidth);
            Physics2D.IgnoreCollision(capsule, childCapsule);
        }
        else
        {
            lineRenderer.SetPosition(1, start);
            capsule.offset = Vector2.zero;
            capsule.size = new Vector2(lineWidth, lineWidth);
        }
    }

    void faceMouse (Vector3 start, Vector3 target)
    {
        // Used to 'point' the capsule collider in the right direction.
        Vector3 diff = target - start;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rot_z - 90);
    }

    void DestroyRecursive ()
    {
        // Sounds much darker than it is.
        if (child != null)
        {
            child.GetComponent<LineToMouse>().DestroyRecursive();
            Destroy(child);
        }
    }

    void OnCollisionEnter2D ()
    {
        collisionCount++;
    }
    void OnCollisionExit2D ()
    {
        collisionCount--;
    }
}
