using UnityEngine;
using System.Diagnostics;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int boidCount = 100;
    public float spawnRadius = 10.0f;

    private Stopwatch stopwatch;

    private void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();  // Iniciar o temporizador

        for (int i = 0; i < boidCount; i++)
        {
            Vector3 position = Random.insideUnitSphere * spawnRadius;
            Instantiate(boidPrefab, position, Quaternion.identity);
        }

        stopwatch.Stop();  // Parar o temporizador
        UnityEngine.Debug.Log("Tempo de execução (serial - CPU): " + stopwatch.ElapsedMilliseconds + "ms");
    }
    public void StartSimulation()
{
    for (int i = 0; i < boidCount; i++)
    {
        Vector3 position = Random.insideUnitSphere * spawnRadius;
        Instantiate(boidPrefab, position, Quaternion.identity);
    }
}

}
