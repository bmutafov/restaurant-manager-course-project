using UnityEngine;

[System.Serializable]
public abstract class Worker : Person
{
	public Sprite avatar;
    public int skill = 1;
    public float salaryPerHour = 10;

    public Worker (string name, int skill) : base(name)
    {
        this.skill = Mathf.Clamp(skill, 1, 10);
		CalculateWorkloadFromSkill();
		DayCycle.Instance.onDayChangedCallback += Pay;
    }

    public abstract void DoWork ();

    public abstract void CalculateWorkloadFromSkill ();

	public virtual void Pay()
	{
		var hours = DayCycle.Instance.WorkingHours;
		float salaryForTheDay = salaryPerHour * hours;
		Budget.Instance.WithdrawFunds(salaryForTheDay);
		Debug.Log("(" + GetType() + ") " + Name + " got payed - " + salaryForTheDay);
	}
}
