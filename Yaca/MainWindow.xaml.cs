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
        private double acc;
        private double bak;

        private int alu_flag;

        public MainWindow()
        {
            InitializeComponent();

            acc = 0;
            bak = acc;
        }

        // hw instructions

        public void SAV()
        {
            bak = acc;
        }

        public void ADD()
        {
            acc += bak;
        }

        public void SUB()
        {
            acc -= bak;
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
            acc = 1;
            DIV();
        }

        public void XSquared()
        {
            SAV();
            MUL();
        }

        public void sqrtX()
        {
            acc = Math.Sqrt(acc); // i give up
        }

        // input hw

        public void Calc()
        {
            if (alu_flag == 0)
            {
                ADD();
            }
    }
}