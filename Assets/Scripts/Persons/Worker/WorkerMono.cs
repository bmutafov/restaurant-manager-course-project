using System.Collections;
using UnityEngine;

public class WorkerMono : MonoBehaviour
{

    public Worker worker;

    private void Update ()
    {
        worker.DoWork();

		if(worker is Waiter)
		{
			Waiter waiter = (Waiter) worker;
			if ( waiter.isServing )
			{
				StartCoroutine(ServeAnimation(waiter));
			}
		}
    }

	IEnumerator ServeAnimation(Waiter waiter)
	{
		Table table = waiter.CurrentOrder.table;
		transform.position = table.transform.position - new Vector3(-2, 1, 0);

		yield return new WaitForSeconds(waiter.ServeMinutes / 3);

		transform.position = waiter.idlePosition;
	}
}
