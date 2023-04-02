namespace Ex05_OtheloForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    // $G$ RUL-001 (-20) Email - Wrong subject format (space between first + last names).
    // $G$ RUL-004 (-40) Wrong zip name format + folder name format (shouldn't have space between first + last names).
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameSettings());
        }
    }
}