using System.Collections.Generic;
using UnityEngine;

public static class Generate
{
    private static readonly List<string> Names = new List<string>(new string[] { "Ivan", "Ayzak", "Kiril" });
    private static readonly List<string> Surrnames = new List<string>(new string[] { "Smith", "Obama", "Micheal" });

    private static readonly float salaryPerSkillIncreasment = 100;

    /// <summary>
    /// Generates a random name and surname from a list of available names
    /// </summary>
    /// <returns>New full name for a person</returns>
    public static string Name ()
    {
        int namesLength = Names.Count;
        int surrnamesLength = Surrnames.Count;

        return Names[Random.Range(0, namesLength)] + " " + Surrnames[Random.Range(0, surrnamesLength)];
    }

    /// <summary>
    /// Generates wealth for a customer on a random algorythm. Different percentages for different wealth groups exist.
    /// </summary>
    /// <returns>Enum Wealthiness</returns>
    public static Wealthiness Wealth ()
    {
        var wealthPercentage = Random.Range(0f, 100f);
        if ( wealthPercentage < 40 )
            return Wealthiness.Poor;
        if ( wealthPercentage >= 40 && wealthPercentage < 80 )
            return Wealthiness.Avarage;
        if ( wealthPercentage >= 80 && wealthPercentage < 95 )
            return Wealthiness.Rich;
        else
            return Wealthiness.Millionaire;
    }


    /// <summary>
    /// Generates a random worker skill from 1-10
    /// </summary>
    /// <returns>Worker skill (int) 1 to 10</returns>
    public static int WorkerSkill ()
    {
        return Random.Range(1, 10);
    }

    /// <summary>
    /// Generates a random worker salary
    /// </summary>
    /// <param name="skill">Skill of the worker (1-10)</param>
    /// <returns>Salary</returns>
    public static float WorkerSalary (int skill)
    {
        var minSalary = skill * salaryPerSkillIncreasment;
        var maxSalary = minSalary + salaryPerSkillIncreasment;
        return Mathf.RoundToInt(Random.Range(minSalary, maxSalary));
    }
}
