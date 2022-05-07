using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using EPPlus.Extensions;
using System.ComponentModel;

namespace ExcelExampleData
{
    public class EPPlusProvier : FileBaseService
    {
        public EPPlusProvier(string fileFullName)
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
            using (var ep = new ExcelPackage(fileStream))
            {
                var ws = ep.Workbook.Worksheets["StudentDB"];
                for (int i = ws.Dimension.Start.Row; i <= ws.Dimension.End.Row; i++)
                {
                    var curRow = ws.Row(i);
                    var cell = ws.Cells[curRow.Row, 1];
                    var firstColval = cell.GetValue<string>();
                    if ((firstColval ?? "")
                        .ToUpper()
                        .Contains(searchName.ToUpper()))
                    {
                        List<StudentInfo> res = new List<StudentInfo>();

                        //for (int col = ws.Dimension.Start.Column; col <= ws.Dimension.End.Column; col++)
                        //{
                        //    res.Add(ws.Cells[curRow.Row, col].GetValue<string>());
                        //}
                        StudentInfo studentInfo = new StudentInfo();
                        studentInfo.name = ws.Cells[curRow.Row, 1].GetValue<string>();
                        studentInfo.rollNo = ws.Cells[curRow.Row, 2].GetValue<string>();
                        studentInfo.shortUrl = ws.Cells[curRow.Row, 3].GetValue<string>();

                        res.Add(studentInfo);

                        return res;
                    }
                }
                return null;
             }
            
        }
    }
    public class StudentInfo
    {
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Roll No.")]
        public string rollNo { get; set; }
        [DisplayName("Alias")]
        public string shortUrl { get; set; }
    }
}
