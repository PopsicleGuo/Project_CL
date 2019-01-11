using System;

namespace Cleanup_Tool
{
    class InfoOutput
    {
        Form1 form = new Form1();

        public void OutPut(string log)
        { 
            if (form.textBox1.GetLineFromCharIndex(form.textBox1.Text.Length) > 1000)
            {
                form.textBox1.Text = "";
            }
            form.textBox1.AppendText(DateTime.Now.ToString("hh:mm:ss      ") + log + "\r\n");
        }
    }
}
