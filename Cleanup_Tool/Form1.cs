using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cleanup_Tool
{
    public partial class Form1 : Form
    {
        RegistryProcesser RegiKey;
        DirProcesser DirHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("Want to wipe frostbite folders in C drive? ",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    string[] RootPath = { @"C:\test", @"C:\Dataset" };

                    try
                    {
                        OutPut("Folder checking....");
                        DirHandler = new DirProcesser();
                        DirHandler.Process(RootPath, (string info) => { OutPut(info); });
                        //DirHandler.DirMoveProcesser((string info) => { OutPut(info); });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else if(checkBox2.Checked && !checkBox1.Checked )
            {
                DialogResult dialogResult = MessageBox.Show("Want to wipe current Frostbite/Drone settings in registry? ", 
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                        OutPut("Registry key checking....");
                        RegiKey = new RegistryProcesser();
                        //RegiKey.CreateSubkey("Test", (string info) => { OutPut(info); });
                        RegiKey.KeyChecker("Test", (string info) => { OutPut(info); });                                   
                }
                else if(dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else if(checkBox1.Checked && checkBox2.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("Want to wipe current folder? ",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    AppProcessHandler TempIns = new AppProcessHandler();
                    TempIns.ProcessChecker("frostbite", (string info) => { OutPut(info); });
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select one option!", "Help Caption", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1,
                                   0, "mspaint.chm",
                                   HelpNavigator.KeywordIndex, "ovals");
                OutPut("Error happened!");
            }
        }

        public void OutPut(string log)
        {
            if (textBox1.GetLineFromCharIndex(textBox1.Text.Length)> 1000)
            {
                textBox1.Text = "";
            }
            textBox1.AppendText(DateTime.Now.ToString("hh:mm:ss      ") + log + "\r\n");
        }
    }
}
