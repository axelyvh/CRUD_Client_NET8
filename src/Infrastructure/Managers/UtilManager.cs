using Application.Exceptions;
using Application.Managers;
using System.Globalization;

namespace Infrastructure.Managers
{
    public class UtilManager : IUtilManager
    {

        public DateTime StringToDate(string datetime)
        {
            try
            {
                return DateTime.Parse(datetime, new CultureInfo("es-PE"));
            }
            catch (Exception)
            {
                throw new UserFriendlyException("La fecha ingresada no es válida");
            }

        }

        public string DateTimeToDateString(DateTime? datetime)
        {
            if (!datetime.HasValue)
            {
                return string.Empty;
            }
            return datetime.Value.ToString("dd/MM/yyyy");
        }

        public byte[] ConvertStreamToByteArray(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}
