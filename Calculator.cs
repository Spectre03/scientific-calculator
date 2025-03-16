using System;
using System.Windows.Forms;

namespace ScientificCalculator
{
    public class Calculator : Form
    {
        private TextBox txtDisplay;
        private string operation;
        private double firstNumber;

        public Calculator()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtDisplay = new TextBox();
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.Location = new System.Drawing.Point(12, 12);
            this.txtDisplay.Size = new System.Drawing.Size(260, 20);
            this.Controls.Add(this.txtDisplay);

            // Create number buttons
            for (int i = 0; i <= 9; i++)
            {
                var btn = new Button();
                btn.Text = i.ToString();
                btn.Location = new System.Drawing.Point(12 + (i % 3) * 60, 50 + (i / 3) * 50);
                btn.Size = new System.Drawing.Size(50, 50);
                btn.Click += NumberButton_Click;
                this.Controls.Add(btn);
            }

            // Create operation buttons
            string[] operations = { "+", "-", "*", "/", "√", "x^y", "log", "sin", "cos", "tan", "!" };
            for (int i = 0; i < operations.Length; i++)
            {
                var btn = new Button();
                btn.Text = operations[i];
                btn.Location = new System.Drawing.Point(200, 50 + i * 50);
                btn.Size = new System.Drawing.Size(50, 50);
                btn.Click += OperationButton_Click;
                this.Controls.Add(btn);
            }

            // Equals and Clear buttons
            var btnEquals = new Button();
            btnEquals.Text = "=";
            btnEquals.Location = new System.Drawing.Point(12, 250);
            btnEquals.Size = new System.Drawing.Size(100, 50);
            btnEquals.Click += BtnEquals_Click;
            this.Controls.Add(btnEquals);

            var btnClear = new Button();
            btnClear.Text = "C";
            btnClear.Location = new System.Drawing.Point(120, 250);
            btnClear.Size = new System.Drawing.Size(100, 50);
            btnClear.Click += BtnClear_Click;
            this.Controls.Add(btnClear);

            // Form properties
            this.Text = "Scientific Calculator";
            this.Size = new System.Drawing.Size(300, 350);
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            txtDisplay.Text += button.Text;
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            firstNumber = double.Parse(txtDisplay.Text);
            operation = button.Text;
            txtDisplay.Clear();
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            double secondNumber = double.Parse(txtDisplay.Text);
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    try
                    {
                        result = firstNumber / secondNumber;
                    }
                    catch (DivideByZeroException)
                    {
                        MessageBox.Show("Cannot divide by zero.");
                        return;
                    }
                    break;
                case "√":
                    result = Math.Sqrt(firstNumber);
                    break;
                case "x^y":
                    result = Math.Pow(firstNumber, secondNumber);
                    break;
                case "log":
                    result = Math.Log(firstNumber);
                    break;
                case "sin":
                    result = Math.Sin(firstNumber);
                    break;
                case "cos":
                    result = Math.Cos(firstNumber);
                    break;
                case "tan":
                    result = Math.Tan(firstNumber);
                    break;
                case "!":
                    result = Factorial((int)firstNumber);
                    break;
            }

            txtDisplay.Text = result.ToString();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Clear();
            firstNumber = 0;
            operation = string.Empty;
        }

        private double Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Calculator());
        }
    }
}