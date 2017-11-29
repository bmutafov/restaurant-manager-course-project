using UnityEngine;

public class WorkerMono : MonoBehaviour
{

    public Worker worker;

    private void Update ()
    {
        worker.DoWork();
    }
}
