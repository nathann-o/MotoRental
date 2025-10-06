public enum Plan
{
    Days7 = 7,
    Days15 = 15,
    Days30 = 30,
    Days45 = 45,
    Days50 = 50
}

public static class PlanExtensions
{
    public static decimal DailyRate(this Plan plan) => plan switch
    {
        Plan.Days7 => 30m,
        Plan.Days15 => 28m,
        Plan.Days30 => 22m,
        Plan.Days45 => 20m,
        Plan.Days50 => 18m,
        _ => throw new DomainException("Unknown plan")
    };
}
