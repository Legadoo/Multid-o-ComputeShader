using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCohesion : MonoBehaviour
{
    public float cohesionRadius = 5.0f;
    public float cohesionStrength = 1.0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, cohesionRadius);
    }

    public Vector3 GetCohesion(Vector3 position)
    {
        Collider[] neighbors = Physics.OverlapSphere(position, cohesionRadius);
        Vector3 cohesion = Vector3.zero;
        int count = 0;

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject)
            {
                cohesion += neighbor.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            cohesion /= count;
            cohesion = (cohesion - position).normalized * cohesionStrength;
        }

        return cohesion;
    }
}
