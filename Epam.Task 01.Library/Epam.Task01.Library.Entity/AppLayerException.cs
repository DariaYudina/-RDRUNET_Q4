using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.Entity
{
    public class AppLayerException : Exception
    {
        public AppLayerException(string message)
            : base(message)
        { }

        public string AppLayer { get; set; }
    }
}

