
public static class KeyCollection
{
    public static bool qwerty = true;
    public static string forwardKey = "w";
    public static string backwardKey = "s";
    public static string leftKey = "q";
    public static string rightKey = "d";
    public static string brakeKey = "left shift";
    public static string interactKey = "e";
    public static string invKey = "i";
    public static string hangarKey = "a";

    public static void Change()
    {
        if (qwerty)
        {
            qwerty = false;
            forwardKey = "z";
            backwardKey = "s";
        }
        else
        {
            qwerty = true;
            forwardKey = "w";
            backwardKey = "s";
        }
    }
}
