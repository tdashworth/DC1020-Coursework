using StringExtensions;
using System;
using System.Windows.Forms;

namespace Scientific_Calculator
{
    public partial class frmCalculator : Form
    {
        enum UserActions { Number, Operator, Function, Other };
        UserActions lastOperation = UserActions.Other;
        double memory = 0;

        public frmCalculator()
        {
            InitializeComponent();
        }

        // Operations and Functions
        private void btnOperator_Click(object sender, EventArgs e)
        {
            Button btnOp = (Button)sender;

            string currentCalculation = tbxCalculationDisplay.Text;

            if (lastOperation != UserActions.Function)
                currentCalculation += tbxNumberDisplay.Text;

            if (lastOperation == UserActions.Operator)
                // Replace last operator with new operator
                currentCalculation = tbxCalculationDisplay.Text.Substring(0, tbxCalculationDisplay.Text.SecondToLastIndexOf(" "));

            Calculate(currentCalculation);
            tbxCalculationDisplay.Text = currentCalculation + " " + (string)btnOp.Tag + " ";

            lastOperation = UserActions.Operator;
        }

        private void btnMathFunction_Click(object sender, EventArgs e)
        {
            Button btnFn = (Button)sender;

            string functionParam = tbxNumberDisplay.Text;

            if (lastOperation == UserActions.Function)
            {
                // Last input was a function so this wraps around it
                var parts = tbxCalculationDisplay.Text.SplitAt(Utils.Positive(tbxCalculationDisplay.Text.LastIndexOf(" "))+1);

                // Updates the value being wrapped from the value in the number textbox
                functionParam = parts[1];

                // Removes that value (string) captured above
                tbxCalculationDisplay.Text = parts[0];
            }

            tbxCalculationDisplay.Text += (string)btnFn.Tag + "(" + functionParam + ")";
            Calculate(tbxCalculationDisplay.Text);

            lastOperation = UserActions.Function;
        }

        // Display methods
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxCalculationDisplay.Text = "";
            tbxNumberDisplay.Text = "";
            lastOperation = UserActions.Other;
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

        // Memory methods
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

        // Format/Other methods
        private void btnNum_Click(object sender, EventArgs e)
        {
            Button btnNum = (Button)sender;

            if (lastOperation != UserActions.Number)
                // Clear previous value if a new number is being entered
                tbxNumberDisplay.Text = "";

            tbxNumberDisplay.Text += btnNum.Text;
            lastOperation = UserActions.Number;
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            if (lastOperation != UserActions.Number)
                // Clear previous value if a new number is being entered
                tbxNumberDisplay.Text = "0";

            if (tbxNumberDisplay.Text.Contains("."))
                // Only one decimal point allowed
                return;

            tbxNumberDisplay.Text += ".";
            lastOperation = UserActions.Number;
        }

        private void btnLeftParenesis_Click(object sender, EventArgs e)
        {
            if (lastOperation == UserActions.Function)
                // Removes that function
                tbxCalculationDisplay.Text = tbxCalculationDisplay.Text.Substring(0, Utils.Positive(tbxCalculationDisplay.Text.LastIndexOf(" ")) + 1);

            tbxCalculationDisplay.Text += "(";
            lastOperation = UserActions.Other;
        }

        private void btnRightParaenesis_Click(object sender, EventArgs e)
        {
            tbxCalculationDisplay.Text += tbxNumberDisplay.Text + ")";
            Calculate(tbxCalculationDisplay.Text);
            lastOperation = UserActions.Other;

        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            tbxNumberDisplay.Text = Math.PI.ToString();
            lastOperation = UserActions.Other;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            string currentCalculation = tbxCalculationDisplay.Text;

            if (lastOperation != UserActions.Function)
                currentCalculation += tbxNumberDisplay.Text;

            Calculate(currentCalculation);
            tbxCalculationDisplay.Text = "";
            lastOperation = UserActions.Other;
        }

        private void Calculate(string calculation)
        {
            try
            {
                if (Utils.ValidBrackets(calculation))
                    tbxNumberDisplay.Text = CalculatorParser.Resolve(calculation).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
