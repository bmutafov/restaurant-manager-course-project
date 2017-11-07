using UnityEngine;

public class WorkerMono : MonoBehaviour
{

    public Worker worker;
    public bool isCook = false;

    private void Start ()
    {
        if ( isCook )
            worker = new Cook("Petar", 6);
        else
            worker = new Waiter("Stanio", 1);
        worker.CalculateWorkloadFromSkill();
    }

    private void Update ()
    {
        worker.DoWork();
    }
}
