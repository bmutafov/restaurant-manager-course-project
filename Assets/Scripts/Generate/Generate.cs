using System.Collections.Generic;
using UnityEngine;

public static class Generate
{
    private static readonly List<string> Names = new List<string>(new string[] { "Ivan", "Ayzak", "Kiril" });
    private static readonly List<string> Surrnames = new List<string>(new string[] { "Smith", "Obama", "Micheal" });

    private static readonly float salaryPerSkillIncreasment = 100;

    public static string Name ()
    {
        int namesLength = Names.Count;
        int surrnamesLength = Surrnames.Count;

        return Names[Random.Range(0, namesLength)] + " " + Surrnames[Random.Range(0, surrnamesLength)];
    }

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

    public static int WorkerSkill ()
    {
        return Random.Range(1, 10);
    }

    public static float WorkerSalary (int skill)
    {
        var minSalary = skill * salaryPerSkillIncreasment;
        var maxSalary = minSalary + salaryPerSkillIncreasment;
        return Mathf.RoundToInt(Random.Range(minSalary, maxSalary));
    }
}
