using UnityEngine;
using System.Diagnostics;

public class BenchmarkManager : MonoBehaviour
{
    private Stopwatch stopwatch;

    // Referências para o controlador da versão serial e paralela
    public BoidManager boidManagerSerial;
    public BoidController boidControllerParallel;

    private void Start()
    {
        // Comparar a versão serial (CPU)
        UnityEngine.Debug.Log("Iniciando teste da versão serial (CPU)...");
        stopwatch = new Stopwatch();
        stopwatch.Start();

        // Chama o código da versão serial
        if (boidManagerSerial != null)
        {
            boidManagerSerial.StartSimulation();  // Suponha que tenha um método para iniciar
        }

        stopwatch.Stop();
        float serialTime = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log("Tempo de execução (serial - CPU): " + serialTime + "ms");

        // Comparar a versão paralela (GPU)
        UnityEngine.Debug.Log("Iniciando teste da versão paralela (GPU)...");
        stopwatch.Reset();
        stopwatch.Start();

        // Chama o código da versão paralela
        if (boidControllerParallel != null)
        {
            boidControllerParallel.StartSimulation();  // Suponha que tenha um método para iniciar
        }

        stopwatch.Stop();
        float parallelTime = stopwatch.ElapsedMilliseconds;
        UnityEngine.Debug.Log("Tempo de execução (paralela - GPU): " + parallelTime + "ms");

        // Calcular o speedup
        float speedup = serialTime / parallelTime;
        UnityEngine.Debug.Log("Speedup: " + speedup);
    }
}
