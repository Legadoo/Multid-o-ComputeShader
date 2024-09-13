using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    public BoidController boidController;

    void Update()
    {
        // Medir e ajustar o desempenho
        float frameRate = 1.0f / Time.deltaTime;
        Debug.Log("FPS: " + frameRate);

        if (frameRate < 30.0f)
        {
            boidController.separationRadius *= 0.9f;
            boidController.alignmentRadius *= 0.9f;
            boidController.cohesionRadius *= 0.9f;
        }
    }
}
