using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework18._06.интерфейсы
{
    public interface IExportable
    {
        string ExportToJson();
        string ExportToXml();
    }
}
