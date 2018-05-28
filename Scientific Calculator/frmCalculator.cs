using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scientific_Calculator
{
    public partial class frmCalculator : Form
    {
        public frmCalculator()
        {
            InitializeComponent();
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btnOp = (Button)sender;
            tbxDisplay.Text += btnOp.Text;
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btnNum = (Button)sender;
            tbxDisplay.Text += btnNum.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxDisplay.Text = "";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string displayText = tbxDisplay.Text;

            if (displayText.Length == 0)
                // No text on the display
                return;

            tbxDisplay.Text = displayText.Substring(0, displayText.Length - 1);
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                tbxDisplay.Text = CalculatorParser.Resolve(tbxDisplay.Text).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
