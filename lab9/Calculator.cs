using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    
        delegate void CalculatorUpdateOutput(Calculator sender, double value, int precision);
        delegate void CalculatorInternalError(Calculator sender, string message);
        enum CalculatorOperation { Add, Sub, Mul, Div }
        enum CalculatorOperation2 { Sqrt, Sqr, Gip, Proc }
        class Calculator
        {
            double? input = null;
            double? result = null;
            bool point = false;
            int? count = null;

            CalculatorOperation? op = null;
            CalculatorOperation2? op2 = null;
            public event CalculatorUpdateOutput didUpdateValue;
            public event CalculatorInternalError InputError;
            public event CalculatorInternalError EqullyError;
            public event CalculatorInternalError MinusSqrtError;
            public void AddDigit(int digit)
            {
                if (input.HasValue && Math.Log10(input.Value) > 9)
                {
                    InputError?.Invoke(this, "Value is too long for this calculator");
                    return;
                }
                if (count > 8)
                {
                    InputError?.Invoke(this, "Value is too long for this calculator");
                    return;
                }
                if (point)
                {
                    count = (count ?? 0);
                    input = ((input ?? 0) * Math.Pow(10, (double)count) + (double)digit / 10) / Math.Pow(10, (double)count);
                    didUpdateValue?.Invoke(this, input.Value, 0);
                    count++;
                }
                else
                {
                    input = (input ?? 0) * 10 + digit;
                    didUpdateValue?.Invoke(this, input.Value, 0);
                }

            }
            public void AddOperation(CalculatorOperation op)
            {

                if (this.op.HasValue)
                {
                    Compute();
                }


                this.op = op;
                if (input.HasValue)
                {
                    result = input;
                    this.Clear();
                }


            }

            public void Compute()
            {
                switch (this.op)
                {
                    case CalculatorOperation.Add:
                        result = result + (input ?? 0);
                        didUpdateValue?.Invoke(this, result.Value, 0);
                        input = null;
                        break;
                    case CalculatorOperation.Div:
                        if (input != null && input.Value != 0)
                        {
                            result = result / (input ?? 0);
                            didUpdateValue?.Invoke(this, result.Value, 0);
                            input = null;
                            break;
                        }
                        else
                        {
                            EqullyError?.Invoke(this, "Division by zero");
                            break;

                        }
                    case CalculatorOperation.Mul:
                        result = result - (input ?? 0);
                        didUpdateValue?.Invoke(this, result.Value, 0);
                        input = null;
                        break;
                    case CalculatorOperation.Sub:
                        result = result * (input ?? 0);
                        didUpdateValue?.Invoke(this, result.Value, 0);
                        input = null;
                        break;



                }

            }

            public void AddOperation2(CalculatorOperation2 op2)
            {

                this.op2 = op2;
                if (input.HasValue)
                {

                    ComOperation2();
                }

            }

            public void ComOperation2()
            {
                switch (this.op2)
                {
                    case CalculatorOperation2.Sqrt:
                        if (input < 0)
                        {
                            InputError?.Invoke(this, "Negative number under the root");
                            return;
                        }
                        input = Math.Sqrt(input ?? 0) * 1.0;
                        didUpdateValue?.Invoke(this, input.Value, 0);
                        break;
                    case CalculatorOperation2.Sqr:
                        input = Math.Pow((input ?? 0), 2);
                        didUpdateValue?.Invoke(this, input.Value, 0);
                        break;
                    case CalculatorOperation2.Gip:
                        input = 1.0 / (input ?? 0);
                        didUpdateValue?.Invoke(this, input.Value, 0);
                        break;
                    case CalculatorOperation2.Proc:
                        input = ((result ?? 0) * input) / 100;
                        didUpdateValue?.Invoke(this, input.Value, 0);
                        break;
                }

            }

            public void Clear()
            {
                input = null;

                didUpdateValue?.Invoke(this, 0, 0);
            }
            public void ClearAll()
            {
                input = null;
                result = null;
                op2 = null;
                op = null;
                didUpdateValue?.Invoke(this, 0, 0);
            }
            public void Point()
            {
                point = true;
            }
            public void Equlay()
            {
                op = null;
                op2 = null;
                input = result;

            }
            public void Polar()
            {
                input = -(input);
                didUpdateValue?.Invoke(this, input.Value, 0);
            }

            public void ClearEnd()
            {
                if (point)
                {
                    input = (int)(((input ?? 0) * Math.Pow(10, (int)count) / 10)) / Math.Pow(10, (int)count - 1);
                    count--;
                    if (count == 0)
                        point = false;
                    didUpdateValue?.Invoke(this, input.Value, 0);
                }
                else
                {
                    input = (int)((input ?? 0) / 10);
                    didUpdateValue?.Invoke(this, input.Value, 0);
                }

            }
            public void CountPointOff()
            {
                count = null;
                point = false;
            }
        }
}

