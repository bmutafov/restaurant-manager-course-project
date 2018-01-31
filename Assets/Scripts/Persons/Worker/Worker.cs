using UnityEngine;

[System.Serializable]
public abstract class Worker : Person
{
	public Sprite avatar;
    public int skill = 1;
    public float salaryPerHour = 10;

    public Worker (string name, int skill) : base(name)
    {
        this.skill = Mathf.Clamp(skill, 1, 10); // Make sure skill cant be over 10 or below 1
		CalculateSalary(); // Calculate the salary
		CalculateWorkloadFromSkill(); // Calculate the workload of the worker (etc. meals per hour for cook )
		DayCycle.Instance.onDayChangedCallback += Pay; // subscribe for the ondaychanged callback, when the worker will receive payment
    }

    public abstract void DoWork ();

    public abstract void CalculateWorkloadFromSkill ();

	public virtual void Pay()
	{
		float salaryForTheDay = salaryPerHour * DayCycle.Instance.WorkingHours; // Calculate the salary for the day 
		Budget.Instance.WithdrawFunds(salaryForTheDay); // Take the funds from the budget
	}

	public virtual void CalculateSalary()
	{
		salaryPerHour = (float)System.Math.Round(skill * Random.Range(1.5f, 4f), 2);
	}
}
