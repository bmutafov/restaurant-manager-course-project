using UnityEngine;

public class WorkerMono : MonoBehaviour
{

    public Worker worker;
    public string position;

    private void Start ()
    {
        if ( position == "Cook" )
        {
            worker = new Cook("Petar", 6);
        }
        else if ( position == "Waiter" )
        {
            worker = new Waiter("Stanio", 1);
        }
        else if ( position == "Host" )
        {
            worker = new Host("Toshko", 2);
        }
        worker.CalculateWorkloadFromSkill();
    }

    private void Update ()
    {
        worker.DoWork();
    }
}
