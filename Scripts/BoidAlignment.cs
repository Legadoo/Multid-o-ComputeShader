using UnityEngine;

public class BoidAlignment : MonoBehaviour
{
    public float alignmentRadius = 5.0f;
    public float alignmentStrength = 1.0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alignmentRadius);
    }

    public Vector3 GetAlignment(Vector3 position)
    {
        Collider[] neighbors = Physics.OverlapSphere(position, alignmentRadius);
        Vector3 alignment = Vector3.zero;
        int count = 0;

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                Boid boid = neighbor.gameObject.GetComponent<Boid>();
                if (boid != null)
                {
                    alignment += boid.Velocity;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            alignment /= count;
            alignment = alignment.normalized * alignmentStrength;
        }

        return alignment;
    }
}
