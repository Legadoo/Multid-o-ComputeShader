using UnityEngine;

public class BoidSeparation : MonoBehaviour
{
    public float separationRadius = 5.0f;
    public float separationStrength = 1.5f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }

    public Vector3 GetSeparation(Vector3 position)
    {
        Collider[] neighbors = Physics.OverlapSphere(position, separationRadius);
        Vector3 separation = Vector3.zero;
        int count = 0;

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                Vector3 direction = position - neighbor.transform.position;
                separation += direction.normalized / direction.magnitude;
                count++;
            }
        }

        if (count > 0)
        {
            separation /= count;
            separation = separation.normalized * separationStrength;
        }

        return separation;
    }
}
