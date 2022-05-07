using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelExampleData
{
    public class NPOIProvider : FileBaseService
    {
        public NPOIProvider(string fileFullName)
            : base(fileFullName)
        {
        }

        public override List<StudentInfo> SearchRow(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return null;
            }
            using (var fileStream = new FileStream(_fullFileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(fileStream);
                var ws = workbook.GetSheet("StudentDB");
                for (int i = ws.FirstRowNum; i <= ws.LastRowNum; i++)
                {
                    var row = ws.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    DataFormatter formatter = new DataFormatter();
                    //search value in first column
                    if ((formatter
                        .FormatCellValue(row.GetCell(row.FirstCellNum, MissingCellPolicy.CREATE_NULL_AS_BLANK)) ?? "")
                        .ToUpper()
                        .Contains(searchName.ToUpper()))
                    {
                        var res = new List<string>();

                        var firstCellNum = row.FirstCellNum;
                        var lastCellNum = row.LastCellNum;
                        for (var col = firstCellNum; col < lastCellNum; col++)
                        {
                            var cell = row.GetCell(col);
                            if (cell == null)
                            {
                                continue;
                            }
                            res.Add(formatter.FormatCellValue(cell));
                        }

                        List<StudentInfo> listRow = new List<StudentInfo>();

                        return listRow;
                    }

                }
                return null;
            }
            
        }
    }
}
