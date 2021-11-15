//MP2 Calculator 
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
            //use a list of string builders to hold each level of grouping
            List<StringBuilder> subEquations = new List<StringBuilder>();
            //add one string builder to the list to represent the whole equation
            subEquations.Add(new StringBuilder());

            string invalidExpression = "Invalid Expression";

            //create a grouping counter to ensure no asymetrical grouping
            int groupingCounter = 0;
            for(int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == '(')
                {
                    groupingCounter++;
                }
                else if(expression[i] == ')')
                {
                    groupingCounter--;
                }
            }
            if(groupingCounter != 0)
            {
                return invalidExpression;
            }

            //create a string builder to hold the final string to be returned
            StringBuilder finalExpression = new StringBuilder();

            //loop through the length of the passed expression
            for(int i = 0; i < expression.Length; i++)
            {
                /* if the current character is an open parenthesis, 
                 * incriment the grouping counter and create a new subgrouping
                 * by adding a new string buildet to the list
                 */
                if (expression[i] == '(')
                {
                    subEquations.Add(new StringBuilder());
                }

                /* if the current character is a close parenthesis,
                 * evaluate the subgrouping, remove it from the list,
                 * and append it to the next highest subgrouping.
                 */
                else if(expression[i] == ')')
                {
                    double temp = Evaluate(subEquations[subEquations.Count - 1].ToString());

                    //make sure calculation was successful
                    if(double.IsNaN(temp))
                    {
                        return invalidExpression;
                    }

                    subEquations.RemoveAt(subEquations.Count - 1);
                    subEquations[subEquations.Count - 1].Append(" ");
                    subEquations[subEquations.Count - 1].Append(temp);
                    subEquations[subEquations.Count - 1].Append(" ");
                }

                //if the current character is any other character, append it to the current subgrouping
                else
                {
                    subEquations[subEquations.Count - 1].Append(expression[i]);
                }
            }

            //create a variable to hold the final value of the expression
            double solution = Evaluate(subEquations[0].ToString());

            //make sure expression was valid
            if (double.IsNaN(solution))
            {
                return invalidExpression;
            }

            /* create standardized spacing in the final expression by removing all spaces
             * and then adding them back between each character
             */
            string tempFinalExpression = expression.Replace(" ", "");
            
            for(int i = 0; i < tempFinalExpression.Length-1; i++)
            {
                if ((char.IsDigit(tempFinalExpression[i]) || tempFinalExpression[i] == '.') && (char.IsDigit(tempFinalExpression[i+1]) || tempFinalExpression[i+1] == '.'))
                {
                    finalExpression.Append(tempFinalExpression[i]);
                }
                else
                {
                    finalExpression.Append(tempFinalExpression[i]);
                    finalExpression.Append(" ");
                }
            }
            finalExpression.Append(tempFinalExpression[^1]);

            //add '=' and the solution, then return the final string
            finalExpression.Append(" = ");
            finalExpression.Append(solution);
            return finalExpression.ToString();

            /* Local function used to evaluate expressions after grouping is handled
             * Returns: evaluated expression as a double or NaN if Invalid Expression
             */
            double Evaluate(string subExpression)
            {
                //create a list of all elements in the expression and remove empty strings
                List<string> parts = new List<string>(subExpression.Split(' '));
                while (parts.Remove("")) ;

                //use try catch to handle situations like 4 + where an opperator does not have numbers to use
                try
                {
                    //evaluate each type of opperation in the correct order
                    EMDAS("^", ref parts);
                    EMDAS("*", ref parts);
                    EMDAS("/", ref parts);
                    EMDAS("+", ref parts);
                    EMDAS("-", ref parts);
                }
                catch (ArgumentException)
                {
                    return double.NaN;
                }

                //make sure that all elements have been used
                if(parts.Count != 1)
                {
                    return double.NaN;
                }

                //use try catch to handle cases like 4+5 where there are no spaces around opperators
                try
                {
                    //return the final value as a double
                    return Convert.ToDouble(parts[0]);
                }
                catch (FormatException)
                {
                    return double.NaN;
                }
            }

            /* Local function used to replace all instances of a specified opperation in a list 
             * with thier values
             */
            void EMDAS (string opperator, ref List<string> parts)
            {
                //while there are still instances of the specified opperator
                while (parts.IndexOf(opperator) != -1)
                {
                    /* create variables to hold:
                     * (currIndex) the index of the first instance of the opperator
                     * (x) the first number involved in the opperation
                     * (y) the second number involved in the opperation
                     * (opSoln) the value of the opperation
                     */
                    int currIndex = parts.IndexOf(opperator);
                    double x;
                    double y;
                    double opSoln = 0;

                    //use try catch to handle situations like 4 + where an opperator does not have numbers to use
                    try
                    {
                        //if the characters on either side of the opperator can be converted to doubles
                        if (double.TryParse(parts[currIndex - 1], out x) && double.TryParse(parts[currIndex + 1], out y))
                        {
                            //carry out the opperation specified by the opperator
                            if (opperator == "^")
                            {
                                opSoln = Math.Pow(x, y);
                            }
                            else if (opperator == "*")
                            {
                                opSoln = x * y;
                            }
                            else if (opperator == "/")
                            {
                                opSoln = x / y;
                            }
                            else if (opperator == "+")
                            {
                                opSoln = x + y;
                            }
                            else if (opperator == "-")
                            {
                                opSoln = x - y;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new ArgumentException("dangling opperator");
                    }
                    //set the first number in the opperation equal to the evaluated opperation
                    parts[currIndex - 1] = opSoln.ToString();

                    //remove the opperator and the second number
                    parts.RemoveRange(currIndex, 2);

                }
            }

        }

        
    }
}
