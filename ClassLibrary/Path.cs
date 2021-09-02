using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Path
    {
        public string PathToDB { get; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Andzej\DataBaseTest.mdf;Integrated Security=True;Connect Timeout=30";

        public Path()
        {
        }
    }
}
