using AbstractValidation;
using CollectionValidation;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.CollectionBLL;
using Epam.Task01.Library.Common;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Task01.Library.ConsolePL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Open();
        }
    }
}
