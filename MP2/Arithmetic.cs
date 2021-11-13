﻿//MP2 Calculator 
//This file contains the Arithmethic class.

//You should implement the requesed method.

using System;
using System.Collections.Generic;
using System.Text;

namespace MP2
{
    public class Arithmetic
    {
        /// <summary>
        /// Use this method as is.
        /// It is called by Main and is used to get an expression from console
        /// which is passed to the Calculate method.
        /// </summary>
        /// <returns>The formatted expression and the expression evaluation result</returns>
        public static string BasicArithmetic()
        {
            Console.WriteLine();
            Console.WriteLine("Basic arithmetic opertions with + - * / ^");
            Console.WriteLine("Enter an expression:");
            string expression = Console.ReadLine().Trim();

            return Calculate(expression);
        }

        /// <summary>
        /// Evaluates the arithmetic expression passed to it and 
        /// returns a nicly formatted expression (proper spaces etc) and 
        /// the result.
        /// The precedence of the operator is enforced only using parenthesis.
        /// </summary>
        /// <returns>
        /// Returns the string that contains the arithmetic expression and the result,
        /// or the requested error message. 
        /// If the expression is not valid, it returns "Invalid expression"
        /// </returns>
        /// <example>
        /// If the expression is "2.1 + 3" then the method returns "2.1 + 3 = 5.1".
        /// If the expression is "(2 + 3) * (2 ^ 5) it returns "( 2 + 3 ) * ( 2 ^ 5 ) = 160" 
        /// If the expression is "2 + ((3 * 2) * 5)" it returns "2 + ( ( 3 * 2 ) * 5 ) = 32" 
        /// Extra spaces are fine, so if the user enters "  2   ^ 3 " then 
        /// the method returns "2 ^ 3 = 8".
        /// If the user enters "4 5" or "4 +" or " (4 + 5" or "4 + 5 * 4)" i.e. any incorrect 
        /// or unbalanced expression, then the method returns "Invalid expression".
        /// </example>
        public static string Calculate(string expression)
        {
            List<StringBuilder> subEquations = new List<StringBuilder>();
            subEquations.Add(new StringBuilder());
            int groupingCounter = 0;
            StringBuilder finalExpression = new StringBuilder();
            for(int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == '(')
                {
                    groupingCounter++;
                    subEquations.Add(new StringBuilder());
                }
                else if(expression[i] == ')')
                {
                    double temp = Evaluate(subEquations[subEquations.Count - 1].ToString());
                    subEquations.RemoveAt(subEquations.Count - 1);
                    subEquations[subEquations.Count - 1].Append(" ");
                    subEquations[subEquations.Count - 1].Append(temp);
                    subEquations[subEquations.Count - 1].Append(" ");
                    groupingCounter--;
                }
                else
                {
                    subEquations[subEquations.Count - 1].Append(expression[i]);
                }
            }
            double solution = Evaluate(subEquations[0].ToString());
            finalExpression.Append(expression.Replace(" ", ""));
            for(int i = 0; i < finalExpression.Length; i++)
            {
                finalExpression.Insert(i * 2 + 1, " ");
            }
            finalExpression.Append("= ");
            finalExpression.Append(solution);
            return finalExpression.ToString();

            double Evaluate(string subExpression)
            {
                List<string> parts = new List<string>(subExpression.Split(' '));
                while (parts.Remove("")) ;


            }

        }

        
    }
}
