/// <summary>
/// Provides string extension methods for applying TextMeshPro rich text tags.
/// These methods wrap a string with TMP-supported markup such as <b>, <i>, <color>, etc.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Wraps the string in a &lt;b&gt; tag to display it in bold
    /// </summary>
    public static string Bold(this string str)
    {
        return $"<b>{str}</b>";
    }

    /// <summary>
    /// Wraps the string in an &lt;i&gt; tag to display it in italic.
    /// </summary>
    public static string Italic(this string str)
    {
        return $"<i>{str}</i>";
    }

    /// <summary>
    /// Wraps the string in a &lt;size&gt; tag to change its font size.
    /// </summary>
    public static string Size(this string str, int size)
    {
        return $"<size={size}>{str}</size>";
    }

    /// <summary>
    /// Appends a newline character ('\n') to the string.
    /// </summary>
    public static string AppendLine(this string str)
    {
        return str + "\n";
    }

    /// <summary>
    /// Wraps the string in a &lt;color&gt; tag with the given color displayName or hex code.
    /// Example: "text".Color("red"), "text".Color("#FF0000")
    /// </summary>
    public static string Color(this string str, string color)
    {
        return $"<color={color}>{str}</color>";
    }

    /// <summary>
    /// Shorthand for applying red color 
    /// </summary>
    public static string Red(this string str)
    {
        return str.Color("red");
    }

    /// <summary>
    /// Shorthand for applying blue color
    /// </summary>
    public static string Blue(this string str)
    {
        return str.Color("blue");
    }

    /// <summary>
    /// Shorthand for applying green color
    /// </summary>
    public static string Green(this string str)
    {
        return str.Color("green");
    }

    /// <summary>
    /// Shorthand for applying yellow color
    /// </summary>
    public static string Yellow(this string str)
    {
        return str.Color("yellow");
    }
}
