//MP2 Calculator 
//This file contains CalculatorMain with the Main method for the Calculator program.

//You should not change this file. Please use as is.

using System;
using System.Collections.Generic;

namespace MP2
{
    public class CalculatorMain
    {
        static void Main()
        {
            Calculus calculator = new Calculus();

            Console.WriteLine("Welcome to the calculator program! Select from the menu:");

            while (true)
            {
                Console.WriteLine();
                Console.Write("(a)rithmetic, (s)et polynomial, (g)et all roots, (p)rint polynomial, (d)ifferentiate, (i)ntegrate, (q)uit? ");
                string choice;

                try
                {
                    choice = Console.ReadLine().Trim().ToLower();
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine($"IO Exception: {e.Message}");
                    continue;
                }

                string result = string.Empty;
                const double accuracy = 0.000001; //For simplcity, accuracy is set to a constant.

                if (choice.StartsWith("a"))
                {
                    result = Arithmetic.BasicArithmetic();
                }
                else if (choice.StartsWith("d"))
                {
                    result = Derivative(calculator);
                }
                else if (choice.StartsWith("i"))
                {
                    result = Integral(calculator);
                }
                else if (choice.StartsWith("g"))
                {
                    try
                    {
                        List<double> roots = calculator.GetAllRoots(accuracy);
                        if (roots.Count == 0)
                        {
                            result = "No real roots found.";
                        }
                        else
                        {
                            for (int i = 0; i < roots.Count; i++)
                            {
                                result += $"root{i} = {roots[i]}\n";
                            }
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        result = e.Message;
                    }
                }
                else if (choice.StartsWith("p"))
                {
                    try
                    {
                        result = calculator.GetPolynomial();
                    }
                    catch (InvalidOperationException e)
                    {
                        result = e.Message;
                    }
                }
                else if (choice.StartsWith("q"))
                {
                    break;
                }
                else if (choice.StartsWith("s"))
                {
                    if (calculator.SetPolynomial())
                    {
                        result = $"Polynomial saved successfully: {calculator.GetPolynomial()}";
                    }
                    else
                    {
                        result = "Not a valid polynomial.";
                    }
                }
                else
                {
                    result = "Please choose from the menu";
                }
                Console.WriteLine();
                Console.WriteLine(result);
            }
        }
        static string Derivative(Calculus calculator)
        {
            string result;

            try
            {
                Console.WriteLine();
                Console.Write($"Calculating derivative of {calculator.GetPolynomial()} evaluated at what x? ");
            }
            catch (InvalidOperationException e)
            {
                return e.Message;
            }

            if (double.TryParse(Console.ReadLine().Trim(), out double x))
            {
                try
                {
                    result = $"result = {calculator.EvaluatePolynomialDerivative(x)}";
                }
                catch (InvalidOperationException e)
                {
                    result = e.Message;
                }
            }
            else
            {
                result = "Not a valid number";
            }

            return result;
        }
        static string Integral(Calculus calculator)
        {
            string result;

            try
            {
                Console.WriteLine();
                Console.WriteLine($"Calculating integral of {calculator.GetPolynomial()} from a to b.");
            }
            catch (InvalidOperationException e)
            {
                return e.Message;
            }

            Console.Write("What is a? ");
            if (!double.TryParse(Console.ReadLine().Trim(), out double a))
            {
                result = "Not a valid number.";
            }
            else
            {
                Console.Write("What is b? ");
                if (!double.TryParse(Console.ReadLine().Trim(), out double b))
                {
                    result = "Not a valid number.";
                }
                else
                {
                    try
                    {
                        result = $"result = {calculator.EvaluatePolynomialIntegral(a, b)}";
                    }
                    catch (InvalidOperationException e)
                    {
                        result = e.Message;
                    }
                }
            }
            return result;
        }
    }
}
