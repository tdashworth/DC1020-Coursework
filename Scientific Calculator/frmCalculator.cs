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
        bool numberBeingEntered = false;

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btnOp = (Button)sender;
            tbxCalculationDisplay.Text += tbxNumberDisplay.Text + btnOp.Text;
            numberBeingEntered = false;
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btnNum = (Button)sender;

            if (!numberBeingEntered)
                // Clear previous value if a new number is being entered
                tbxNumberDisplay.Text = "";

            tbxNumberDisplay.Text += btnNum.Text;
            numberBeingEntered = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxCalculationDisplay.Text = "";
            tbxNumberDisplay.Text = "";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string displayText = tbxNumberDisplay.Text;

            if (displayText.Length == 0)
                // No text on the display
                return;

            tbxNumberDisplay.Text = displayText.Substring(0, displayText.Length - 1);
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                tbxCalculationDisplay.Text += tbxNumberDisplay.Text;
                tbxNumberDisplay.Text = CalculatorParser.Resolve(tbxCalculationDisplay.Text).ToString();
                tbxCalculationDisplay.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO MC - Clear memory to 0
        // TODO MR - Append memory to display
        // TODO M+ - Resolve and add to memory
        // TODO M- - Resolve and subtract from memory

        // TODO Exponents - x^2, x^3?, x^y
        // TODO Roots - √(x), 3√(x), y√(x)
        // TODO Sine - sin(x), sinh(x) 
        // TODO Cosine - cos(x), cosh(x)
        // TODO Tangent - tan(x), tanh(x)
        // TODO Other - Exp, Mod, log, 10^x, n!, 1/(x)

        // TODO Sign - +/-
        // TODO Add CE clear function - clears number display
    
    }
}
