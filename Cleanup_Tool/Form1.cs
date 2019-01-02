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
        ButtonController ButtonC = new ButtonController();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //Check User check box status for future operation
            if (checkBox1.Checked && !checkBox2.Checked)
            {   //Pop up warning message before starting
                DialogResult dialogResult = MessageBox.Show("Want to wipe frostbite folders in C drive? ",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    string[] rootPath = { @"C:\" };

                    try
                    {
                        OutPut("Folder checking....");
                        DirHandler = new DirProcesser();
                        DirHandler.Process(rootPath, (string info) => { OutPut(info); });
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
                    string[] rootPath = { @"C:\" };
                    try
                    {
                        OutPut("Registry key checking....");
                        RegiKey = new RegistryProcesser();
                        RegiKey.KeyChecker("FrostEd", (string info) => { OutPut(info); });
                        DirHandler = new DirProcesser();
                        DirHandler.Process(rootPath, (string info) => { OutPut(info); });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                        
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
                    OutPut("Start cleanup process!");
                    RegiKey = new RegistryProcesser();
                    RegiKey.KeyChecker("FrostEd", (string info) => { OutPut(info); });
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                ButtonC.NoneCheck(this);
            }
        }
        //Create this method for info output operation
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
