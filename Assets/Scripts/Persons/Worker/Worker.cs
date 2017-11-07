using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Worker : Person
{
    public int skill = 1;
    public float salaryPerHour = 10;

    public Worker (string name, int skill) : base(name)
    {
        this.skill = Mathf.Clamp(skill, 1, 10);
    }

    public abstract void DoWork ();

    public abstract void CalculateWorkloadFromSkill ();
}
