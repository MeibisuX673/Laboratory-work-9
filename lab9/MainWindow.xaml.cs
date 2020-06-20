using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculator calc;
        public MainWindow()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.didUpdateValue += CalculatorDidUpdateValue;
            calc.InputError += CalculatorInputError;
            calc.EqullyError += CalculatorInternalError;
            calc.MinusSqrtError += CalculatorMinusError;
            calc.Clear();
        }
        private void CalculatorMinusError(Calculator sender, string message)
        {
            MessageBox.Show(message, "Error");
        }

        private void CalculatorInputError(Calculator sender, string message)
        {
            MessageBox.Show(message, "Error");
        }

        private void CalculatorDidUpdateValue(Calculator sender, double value, int precision)
        {
            label1.Content = value.ToString();
        }

        private void CalculatorInternalError(Calculator sender, string message)
        {
            MessageBox.Show(message, "Error");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int digit = -1;
            Button b = sender as Button;
            if (int.TryParse(b.Content.ToString(), out digit))
            {

                calc.AddDigit(digit);
            }
            else
            {
                switch (b.Name)
                {

                    case "minus":
                        calc.CountPointOff();
                        calc.AddOperation(CalculatorOperation.Mul);

                        break;
                    case "plus":
                        calc.AddOperation(CalculatorOperation.Add);
                        calc.CountPointOff();
                        break;
                    case "clear":
                        calc.CountPointOff();
                        calc.Clear();
                        break;
                    case "div":
                        calc.AddOperation(CalculatorOperation.Div);
                        calc.CountPointOff();
                        break;
                    case "mult":
                        calc.AddOperation(CalculatorOperation.Sub);
                        calc.CountPointOff();
                        break;
                    case "equally":

                        calc.Compute();
                        calc.Equlay();
                        calc.CountPointOff();
                        break;
                    case "ClearAll":
                        calc.ClearAll();
                        calc.CountPointOff();
                        break;
                    case "point":

                        this.label1.Content += ",";
                        calc.Point();
                        break;
                    case "sqrt":
                        calc.CountPointOff();
                        calc.AddOperation2(CalculatorOperation2.Sqrt);
                        break;
                    case "sqr":
                        calc.CountPointOff();
                        calc.AddOperation2(CalculatorOperation2.Sqr);
                        break;
                    case "gip":
                        calc.CountPointOff();
                        calc.AddOperation2(CalculatorOperation2.Gip);
                        break;
                    case "polar":
                        calc.CountPointOff();
                        calc.Polar();
                        break;
                    case "clearEnd":
                        calc.ClearEnd();
                        break;
                    case "procent":
                        calc.CountPointOff();
                        calc.AddOperation2(CalculatorOperation2.Proc);
                        break;
                    default:
                        break;
                }

            }

        }

    }
}
