using StringExtensions;
using System;
using System.Windows.Forms;

namespace Scientific_Calculator
{
    public partial class frmCalculator : Form
    {
        enum Operations { Number, Operator, Function, Clear };
        Operations lastOperation = Operations.Clear;
        double memory = 0;

        public frmCalculator()
        {
            InitializeComponent();
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btnOp = (Button)sender;

            string currentCalculation = tbxCalculationDisplay.Text;

            if (lastOperation != Operations.Function)
                currentCalculation += tbxNumberDisplay.Text;

            if (lastOperation == Operations.Operator)
                // Replace last operator with new operator
                currentCalculation = tbxCalculationDisplay.Text.Substring(0, tbxCalculationDisplay.Text.SecondToLastIndexOf(" "));

            Calculate(currentCalculation);
            tbxCalculationDisplay.Text = currentCalculation + " " + btnOp.Text + " ";

            lastOperation = Operations.Operator;
        }

        private void btnMathFunction_Click(object sender, EventArgs e)
        {
            Button btnFn = (Button)sender;

            string functionParam = tbxNumberDisplay.Text;

            if (lastOperation == Operations.Function)
            {
                // Last input was a function so this wraps around it

                functionParam = tbxCalculationDisplay.Text.Substring(
                    Utils.Positive(tbxCalculationDisplay.Text.LastIndexOf(" ")),
                    Utils.Positive(tbxCalculationDisplay.Text.Length)
                );

                tbxCalculationDisplay.Text = tbxCalculationDisplay.Text.Substring(0, Utils.Positive(tbxCalculationDisplay.Text.LastIndexOf(" ")));
            }

            tbxCalculationDisplay.Text += (string)btnFn.Tag + "(" + functionParam + ")";
            Calculate(tbxCalculationDisplay.Text);

            lastOperation = Operations.Function;
        }

        private void btnStringFunction_Click(object sender, EventArgs e)
        {
            Button btnFn = (Button)sender;
            
            tbxCalculationDisplay.Text += String.Format((string)btnFn.Tag, tbxNumberDisplay.Text);
            Calculate(tbxCalculationDisplay.Text);

            lastOperation = Operations.Function;
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btnNum = (Button)sender;

            if (lastOperation != Operations.Number)
                // Clear previous value if a new number is being entered
                tbxNumberDisplay.Text = "";

            tbxNumberDisplay.Text += btnNum.Text;
            lastOperation = Operations.Number;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            string currentCalculation = tbxCalculationDisplay.Text;

            if (lastOperation != Operations.Function)
                currentCalculation += tbxNumberDisplay.Text;

            Calculate(currentCalculation);
            tbxCalculationDisplay.Text = "";
            lastOperation = Operations.Clear;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxCalculationDisplay.Text = "";
            tbxNumberDisplay.Text = "";
            lastOperation = Operations.Clear;
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            tbxNumberDisplay.Text = "";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string displayText = tbxNumberDisplay.Text;

            tbxNumberDisplay.Text = displayText.Substring(0, Utils.Positive(displayText.Length - 1));
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

        private void Calculate(string calculation)
        {
            try
            {
                tbxNumberDisplay.Text = CalculatorParser.Resolve(calculation).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO Roots - √(x), 3√(x), y√(x)
        // TODO Other - Exp, Mod

        // TODO Sign - +/-
    }
}
