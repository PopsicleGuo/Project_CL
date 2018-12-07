using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cleanup_Tool
{
    class InfoOutput
    {
        public void OutPut(string log, Form1 obj)
        {
            if (obj.textBox1.GetLineFromCharIndex(obj.textBox1.Text.Length) > 1000)
            {
                obj.textBox1.Text = "";
            }
            obj.textBox1.AppendText(DateTime.Now.ToString("hh:mm:ss      ") + log + "\r\n");
        }
    }
}
