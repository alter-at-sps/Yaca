using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yaca
{
    // for introduction to the Yaca internal architecture, please reference https://www.zachtronics.com/images/TIS-100P%20Reference%20Manual.pdf.
    // note: only the T21 Basic Execution Node (as described by the TIS-100 reference document) is partialy supported.

    public partial class MainWindow : Window
    {
        private string text_input;

        private double acc;
        private double bak;

        private int alu_flag;

        public MainWindow()
        {
            InitializeComponent();

            acc = 0;
            bak = acc;

            alu_flag = 0;
            text_input = "";
        }

        // hw instructions

        public void SAV()
        {
            bak = acc;
        }

        public void SWP()
        {
            double hw_temp = acc;
            acc = bak;
            bak = hw_temp;
        }

        public void ADD()
        {
            acc += bak;
        }

        public void SUB()
        {
            acc = bak - acc;
        }

        public void MUL()
        {
            acc *= bak;
        }

        public void DIV()
        {
            acc = bak / acc;
        }

        public void NEG()
        {
            acc = -acc;
        }

        // jump routines

        public void C()
        {
            acc = 0;
        }

        public void CE()
        {
            C();
            SAV();
        }

        public void oneOverX()
        {
            double stack = bak;

            SAV();
            acc = 1;
            SWP();
            DIV();

            bak = stack;
        }

        public void XSquared()
        {
            double stack = bak;

            SAV();
            MUL();

            bak = stack;
        }

        public void sqrtX()
        {
            acc = Math.Sqrt(acc); // i give up
        }

        public void XPercentile()
        {
            if (alu_flag == 0)
            {
                acc = 0;
                return;
            }

            double stack = bak;

            SAV();
            acc = 100;
            DIV();

            if (alu_flag == 1 || alu_flag == 2)
            {
                bak = stack;
                MUL();
            }

            bak = stack;
        }

        // input hw

        private void UpdateAccDisplay()
        {
            AccDisplay.Text = acc.ToString();

            /* if (bak != 0)
            {
                BakDisplay.Text = $"BAK: {bak}";
            } else
            {
                BakDisplay.Text = "";
            } */
        }

        private void UpdateTextInputDisplay()
        {
            AccDisplay.Text = text_input;

            /* if (bak != 0)
            {
                BakDisplay.Text = $"BAK: {bak}";
            }
            else
            {
                BakDisplay.Text = "";
            } */
        }

        private void EnsureAccInputIsConverted()
        {
            if (text_input == "") return; // already converted / no input to convert

            acc = double.Parse(text_input);
            text_input = "";
        }

        private void InputCalc(object sender, RoutedEventArgs e)
        {
            EnsureAccInputIsConverted();

            switch (alu_flag)
            {
                case 0:
                    break; // no two term op queued

                case 1:
                    ADD();
                    break;

                case 2:
                    SUB();
                    break;

                case 3:
                    MUL();
                    break;

                case 4:
                    DIV();
                    break;
            }

            bak = 0;
            alu_flag = 0; // clear op
            text_input = "";

            UpdateAccDisplay();
        }

        private void PushTwoTermOp(int op)
        {
            EnsureAccInputIsConverted();

            SAV();
            acc = 0;

            alu_flag = op; // set op mode
            // UpdateAccDisplay();
        }

        private void InputAdd(object sender, RoutedEventArgs e)
        {
            PushTwoTermOp(1);
        }

        private void InputSub(object sender, RoutedEventArgs e)
        {
            PushTwoTermOp(2);
        }

        private void InputMul(object sender, RoutedEventArgs e)
        {
            PushTwoTermOp(3);
        }

        private void InputDiv(object sender, RoutedEventArgs e)
        {
            PushTwoTermOp(4);
        }

        private void InputNeg(object sender, RoutedEventArgs e)
        {
            EnsureAccInputIsConverted();
            NEG();
            UpdateAccDisplay();
        }

        private void InputOneOver(object sender, RoutedEventArgs e)
        {
            EnsureAccInputIsConverted();
            oneOverX();
            UpdateAccDisplay();
        }

        private void InputSquared(object sender, RoutedEventArgs e)
        {
            EnsureAccInputIsConverted();
            XSquared();
            UpdateAccDisplay();
        }

        private void InputSqrt(object sender, RoutedEventArgs e)
        {
            EnsureAccInputIsConverted();
            sqrtX();
            UpdateAccDisplay();
        }

        private void InputClear(object sender, RoutedEventArgs e)
        {
            text_input = "";
            acc = 0;
            UpdateAccDisplay();
        }

        private void InputClearExpression(object sender, RoutedEventArgs e)
        {
            text_input = "";
            acc = 0;
            SAV();

            alu_flag = 0;
            UpdateAccDisplay();
        }

        private void InputPercentile(object sender, RoutedEventArgs e)
        {
            EnsureAccInputIsConverted();
            XPercentile();
            UpdateAccDisplay();
        }

        // number input

        private void Input0(object sender, RoutedEventArgs e)
        {
            text_input += '0';
            UpdateTextInputDisplay();
        }

        private void Input1(object sender, RoutedEventArgs e)
        {
            text_input += '1';
            UpdateTextInputDisplay();
        }

        private void Input2(object sender, RoutedEventArgs e)
        {
            text_input += '2';
            UpdateTextInputDisplay();
        }

        private void Input3(object sender, RoutedEventArgs e)
        {
            text_input += '3';
            UpdateTextInputDisplay();
        }

        private void Input4(object sender, RoutedEventArgs e)
        {
            text_input += '4';
            UpdateTextInputDisplay();
        }

        private void Input5(object sender, RoutedEventArgs e)
        {
            text_input += '5';
            UpdateTextInputDisplay();
        }

        private void Input6(object sender, RoutedEventArgs e)
        {
            text_input += '6';
            UpdateTextInputDisplay();
        }

        private void Input7(object sender, RoutedEventArgs e)
        {
            text_input += '7';
            UpdateTextInputDisplay();
        }

        private void Input8(object sender, RoutedEventArgs e)
        {
            text_input += '8';
            UpdateTextInputDisplay();
        }

        private void Input9(object sender, RoutedEventArgs e)
        {
            text_input += '9';
            UpdateTextInputDisplay();
        }

        private void InputSeparator(object sender, RoutedEventArgs e)
        {
            if (text_input.Contains(",")) return; // already inputed a decimal separator

            text_input += ',';
            UpdateTextInputDisplay();
        }

        private void InputBackspace(object sender, RoutedEventArgs e)
        {
            if (text_input.Length == 0) return;
            
            text_input = text_input.Substring(0, text_input.Length - 1);
            UpdateTextInputDisplay();
        }
    }
}