using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelExampleData
{
    public abstract class FileBaseService : IExcelService
    {
        protected readonly string _fullFileName;

        public FileBaseService(string fullFileName)
        {
            _fullFileName = fullFileName;
        }

        public abstract List<StudentInfo> SearchRow(string searchName);
    }
}
