namespace ApplicationFormPractice.SharedKernel;

public static class HelperMethods
{
    public static string ListToString(this List<string> choices)
    {
        return choices != null ? string.Join(",", choices) : string.Empty;
    }
}
