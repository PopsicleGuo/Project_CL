using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cleanup_Tool
{
    class ButtonController
    {
        InfoOutput info = new InfoOutput();
        RegistryProcesser RegiKey;


        public void RegistryKey(Form1 form)
        {
            DialogResult dialogResult = MessageBox.Show("Want to wipe current folder? ",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                info.OutPut("Start cleanup process!");
                RegiKey = new RegistryProcesser();
                RegiKey.KeyChecker("FrostEd", (string text) => { form.OutPut(text); });
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        public void NoneCheck()
        {
            MessageBox.Show("Please select one option!", "Help Caption", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1,
                                   0, "mspaint.chm",
                                   HelpNavigator.KeywordIndex, "ovals");
            info.OutPut("Error Happened!! ");
        }
    }
}
