﻿using StringExtensions;
using System;
using System.Windows.Forms;
using static Scientific_Calculator.Utils;

namespace Scientific_Calculator
{
    public partial class frmCalculator : Form
    {
        /// <summary>
        /// lastOperation is used to capture the last "type" of user action. This allows for some basic validation as the user builds the expression.
        /// These include only allowing one operator between operands therefore 2+-1 is not valid (negate function should be used to enter negation numbers
        /// </summary>
        enum UserActions { Number, Operator, Function, Other };
        UserActions lastOperation = UserActions.Other;

        AngleMode angleMode = AngleMode.Rad;
        double memory = 0;

        public frmCalculator()
        {
            InitializeComponent();
        }

        // Operations and Functions (Generic)
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

        private void btnFunction_Click(object sender, EventArgs e)
        {
            Button btnFn = (Button)sender;

            string functionParam = tbxNumberDisplay.Text;

            if (lastOperation == UserActions.Function)
            {
                // Last input was a function so this wraps around it
                var parts = tbxCalculationDisplay.Text.SplitAt(Utils.Positive(tbxCalculationDisplay.Text.LastIndexOf(" ")));

                // Updates the value being wrapped from the value in the number textbox
                functionParam = parts[1].Trim();

                // Removes that value (string) captured above
                tbxCalculationDisplay.Text = parts[0];
            }

            tbxCalculationDisplay.Text += " " + (string)btnFn.Tag + "(" + functionParam + ")";
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
                tbxNumberDisplay.Text = memory.ToString();
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
                tbxNumberDisplay.Text = memory.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Formating/Other methods
        // Generic
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
            if (lastOperation == UserActions.Number)
                tbxCalculationDisplay.Text += tbxNumberDisplay.Text;

            tbxCalculationDisplay.Text += ")";
            Calculate(tbxCalculationDisplay.Text);
            lastOperation = UserActions.Function;
        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            tbxNumberDisplay.Text = Math.PI.ToString();
            lastOperation = UserActions.Other;
        }

        private void btnAngleMode_Click(object sender, EventArgs e)
        {
            switch (angleMode)
            {
                case AngleMode.Rad:
                    angleMode = AngleMode.Deg;
                    break;
                case AngleMode.Deg:
                    angleMode = AngleMode.Grad;
                    break;
                case AngleMode.Grad:
                    angleMode = AngleMode.Rad;
                    break;
            }

            btnAngleMode.Text = angleMode.ToString().ToUpper();
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
                tbxNumberDisplay.Text = CalculatorParser.Resolve(calculation, angleMode).ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message != "Invalid brackets")
                    DisplayError(ex.Message);
            }
        }

        private void DisplayError(string message)
        {
            tbxNumberDisplay.Text = message;
            lastOperation = UserActions.Other;
        }
    }
}
