namespace ApplicationFormPractice.SharedKernel.Enums;

public static class EnumHelper
{
    public static List<string> GetEnumMembers<T>() where T : Enum
    {
        var names = Enum.GetNames(typeof(T));

        return new List<string>(names);
    }
}
