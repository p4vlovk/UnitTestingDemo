namespace DemoCode;

public class NameJoiner
{
    public int MyProperty { get; set; }

    public static string Join(string firstName, string lastName) => $"{firstName} {lastName}";
}