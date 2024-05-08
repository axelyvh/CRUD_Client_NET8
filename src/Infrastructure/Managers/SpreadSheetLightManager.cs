using Application.Managers;
using SpreadsheetLight;
using System.Data;

namespace Infrastructure.Managers
{
    public class SpreadSheetLightManager : IExcelManager
    {

        public MemoryStream Generate(DataTable dt, List<string>? cellBolds = null)
        {


            MemoryStream ms = new MemoryStream();
            using (SLDocument sLDocument = new SLDocument())
            {
                sLDocument.ImportDataTable(1, 1, dt, true);

                if (cellBolds != null && cellBolds.Any())
                {
                    foreach (string cellBold in cellBolds)
                    {
                        SLStyle style = sLDocument.CreateStyle();
                        style.Font.Bold = true;
                        sLDocument.SetCellStyle(cellBold, style);
                    }
                }

                sLDocument.SaveAs(ms);
            }
            ms.Position = 0;

            return ms;

        }

    }
}
