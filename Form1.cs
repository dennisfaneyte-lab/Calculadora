using System;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        private double firstValue = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonNumber_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b == null) return;
            if (isNewEntry || txtDisplay.Text == "0")
            {
                txtDisplay.Text = b.Text;
                isNewEntry = false;
            }
            else
            {
                txtDisplay.Text += b.Text;
            }
        }

        private void ButtonDecimal_Click(object sender, EventArgs e)
        {
            // Usa coma según configuración regional en la entrada
            if (!txtDisplay.Text.Contains(","))
            {
                txtDisplay.Text += ",";
                isNewEntry = false;
            }
        }

        private void ButtonOperator_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b == null) return;
            double.TryParse(txtDisplay.Text, out firstValue);
            currentOperator = b.Tag?.ToString() ?? "";
            isNewEntry = true;
        }

        private void ButtonEqual_Click(object sender, EventArgs e)
        {
            double second = 0;
            double.TryParse(txtDisplay.Text, out second);

            double result = 0;
            bool ok = true;
            switch (currentOperator)
            {
                case "+":
                    result = firstValue + second;
                    break;
                case "-":
                    result = firstValue - second;
                    break;
                case "*":
                    result = firstValue * second;
                    break;
                case "/":
                    if (second == 0)
                    {
                        MessageBox.Show("División por cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ok = false;
                    }
                    else result = firstValue / second;
                    break;
                default:
                    result = second;
                    break;
            }

            if (ok)
            {
                txtDisplay.Text = result.ToString();
                isNewEntry = true;
                currentOperator = "";
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            firstValue = 0;
            currentOperator = "";
            isNewEntry = true;
        }
    }
}
