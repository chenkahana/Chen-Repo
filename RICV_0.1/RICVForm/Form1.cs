using RICV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RICVForm
{
    public partial class Form1 : Form
    {

        public RICVBase _RICVLogic;
        public ListView _listView;

        public Form1()
        {
            InitializeComponent();
        }

        private void preformActionByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //Show the relevent combobox
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //Crate RICV object by all combo box paramiters
          //  _listView = _RICVLogic.getView();

        }
    }
}
