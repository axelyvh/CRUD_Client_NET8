namespace Application.Managers
{
    public interface IUtilManager
    {
        DateTime StringToDate(string datetime);
        byte[] ConvertStreamToByteArray(Stream stream);
        string DateTimeToDateString(DateTime? datetime);
    }
}
