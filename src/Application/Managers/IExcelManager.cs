using System.Data;

namespace Application.Managers
{
    public interface IExcelManager
    {
        MemoryStream Generate(DataTable dt, List<string>? cellBolds = null);
    }
}