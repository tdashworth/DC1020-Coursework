using System;
using System.Windows.Forms;

namespace Scientific_Calculator
{
    public partial class frmCalculator : Form
    {
        bool isNumberBeingEntered = false;
        string previousEntry = "";
        double memory = 0;

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btnOp = (Button)sender;

            if (previousEntry.Length > 0)
                // Removes the previous entry because two operation buttons were pressed 
                tbxCalculationDisplay.Text = tbxCalculationDisplay.Text.Substring(0, tbxCalculationDisplay.Text.Length - previousEntry.Length);

            previousEntry = tbxNumberDisplay.Text + btnOp.Text;
            tbxCalculationDisplay.Text += previousEntry;
            isNumberBeingEntered = false;
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btnNum = (Button)sender;

            if (!isNumberBeingEntered)
                // Clear previous value if a new number is being entered
                tbxNumberDisplay.Text = "";

            tbxNumberDisplay.Text += btnNum.Text;
            isNumberBeingEntered = true;
            previousEntry = "";
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                tbxCalculationDisplay.Text += tbxNumberDisplay.Text;
                tbxNumberDisplay.Text = CalculatorParser.Resolve(tbxCalculationDisplay.Text).ToString();
                tbxCalculationDisplay.Text = "";
                previousEntry = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxCalculationDisplay.Text = "";
            tbxNumberDisplay.Text = "";
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
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

        private void btnMemoryClear_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void btnMemoryRecall_Click(object sender, EventArgs e)
        {
            tbxNumberDisplay.Text = memory.ToString();
        }

        private void btnMemoryAddition_Click(object sender, EventArgs e)
        {
            try
            {
                double numberInDisplay = Double.Parse(tbxNumberDisplay.Text);
                memory += numberInDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMemorySubtraction_Click(object sender, EventArgs e)
        {
            try
            {
                double numberInDisplay = Double.Parse(tbxNumberDisplay.Text);
                memory -= numberInDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO Exponents - x^2, x^3?, x^y
        // TODO Roots - √(x), 3√(x), y√(x)
        // TODO Sine - sin(x), sinh(x) 
        // TODO Cosine - cos(x), cosh(x)
        // TODO Tangent - tan(x), tanh(x)
        // TODO Other - Exp, Mod, log, 10^x, n!, 1/(x)

        // TODO Sign - +/-
    }
}
