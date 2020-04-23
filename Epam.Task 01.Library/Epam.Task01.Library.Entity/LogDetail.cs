using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class LogDetail
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public string Login { get; set; }

        public string AppLayer { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string StackTrace { get; set; }
    }
}
