namespace MongoDB.Models.Store;

using System.Globalization;

public static class Methods
{
    private const string Language = "en-US";
    private const string MessageError = "¡El campo no es correcto!";

    public static bool IsInt(string number)
    {
        int intent;
        try
        {
            intent = Convert.ToInt32(number, new CultureInfo(Language));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static int CheckInt(string number, string message)
    {
        try
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (IsInt(number))
                {
                    int numberInt = Convert.ToInt32(number, new CultureInfo(Language));
                    if (numberInt >= 0)
                        return numberInt;
                    else
                        Dialogs.GenerateMessage(MessageBoxImage.Warning, message);
                }
                else
                    Dialogs.GenerateMessage(MessageBoxImage.Warning, MessageError);
            }
        }
        catch (Exception)
        {
            Dialogs.GenerateMessage(MessageBoxImage.Warning, MessageError);
        }
        return 0;
    }
}
