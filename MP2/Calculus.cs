//MP2 Calculator 
//This file contains the CalculusCalculator class.

//You should implement the requesed methods.


using System;
using System.Collections.Generic;
using System.Text;

namespace MP2
{
    public class Calculus
    {
        List<double> coefficientList = new List<double>(); //the only field of this class

        //You are allowed to add constructors to be able to unit test.

        /// <summary>
        /// Prompts the user for the coefficients of a polynomial, and sets the 
        /// the coefficientList field of the object.
        /// The isValidPolynomial method is used to check for the validity
        /// of the polynomial entered by the user, otherwise the field must 
        /// not change.
        /// The acceptable format of the coefficients received from the user is 
        /// a series of numbers (one for each coefficient) separated by spaces. 
        /// All coefficients values must be entered even those that are zero.
        /// </summary>
        /// <returns>True if the polynomial is succeffully set, false otherwise.</returns>
        public bool SetPolynomial()
        {

        }

        /// <summary>
        /// Checks if the passed polynomial string is valid.
        /// The acceptable format of the coefficient string is a series of 
        /// numbers (one for each coefficient) separated by spaces. 
        /// </summary>
        /// <example>
        /// Examples of valid strings: "2   3.5 0  ", or "-2 -3.5 0 0"
        /// Examples of invalid strings: "3..5", or "2x^2+1", or "a b c", or "3 - 5"
        /// </example>
        /// <param name="polynomial">
        /// A string containing the coefficient of a polynomial. The first value is the
        /// highest order, and all coefficients exist (even 0's).
        /// </param>
        /// <returns>True if a valid polynomial, false otherwise.</returns>
        public bool IsValidPolynomial(string polynomial)
        {
             
        }

        /// <summary>
        /// Returns a string representing this polynomial.
        /// </summary>
        /// <returns>
        /// A string containing the polynomial in the format:
        /// (a_n)*x^n + (a_n_1)*x^n_1 + ... + (a1)*x + (a0) 
        /// Note that for simplicity, each coefficient is surrounded by 
        /// parenthesis (for us to easily consider negative coefficients too).
        /// It does not display the term of any coefficient that is 0.
        /// If all coefficients are 0, then it returns "0".
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public string GetPolynomial()
        {
             
        }

        /// <summary>
        /// Evaluates this polynomial at the x passed to the method.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial.</param>
        /// <returns>The result of the polynomial evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomial(double x)
        {
             
        }

        /// <summary>
        /// Finds a root of this polynomial using the provided guess.
        /// </summary>
        /// <param name="guess">The initial value for the Newton method.</param>
        /// <param name="epsilon">The desired accuracy: stops when |f(result)| is
        /// less than or equal epsilon.</param>
        /// <param name="iterationMax">A max cap on the number of iterations in the
        /// Newton-Raphson method. This is to also guarantee no infinite loops.
        /// If this iterationMax is reached, a double.NaN is returned.</param>
        /// <returns>
        /// The root found using the Netwon-Raphson method. 
        /// A double.NaN is returned if a root cannot be found.
        /// The return value is rounded to have 4 digits after the decimal point.
        /// </returns>
        public double NewtonRaphson(double guess, double epsilon, int iterationMax)
        {
            int count = 0;
            double x = guess;

            while (Math.Abs(EvaluatePolynomial(x)) > epsilon && count < iterationMax)
            {
                x = x - EvaluatePolynomial(x) / EvaluatePolynomialDerivative(x);
                count++;
            }

            if (count == iterationMax || double.IsInfinity(x)) 
            {
                return double.NaN;
            }

            return Math.Round(x, 4); //4 decimal places
        }

        /// <summary>
        /// Calculates and returns all unique real roots of this polynomial 
        /// that can be found using the NewtonRaphson method. 
        /// The method uses all initial guesses between -50 and 50 with 
        /// steps of 0.5 to find all unique roots it can find. 
        /// A root is considered unique, if there is no root already found 
        /// that is within an accuracy level of 0.001 (since we rounded the roots).
        /// Uses 10 as the max number of iterations used by Newton-Raphson method.
        /// </summary>
        /// <param name="epsilon">The desired accuracy.</param>
        /// <returns>A list containing all the unique roots that the method finds.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public List<double> GetAllRoots(double epsilon)
        {
             
        }

        /// <summary>
        /// Evaluates the 1st derivative of this polynomial at x, passed to the method.
        /// The method uses the exact numerical technique, since it is easy to obtain the 
        /// derivative of a polynomial.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial derivative.</param>
        /// <returns>The result of the polynomial derivative evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialDerivative(double x)
        {
             
        }

        /// <summary>
        /// Evaluates the definite integral of this polynomial from a to b.
        /// The method uses the exact numerical technique, since it is easy to obtain the 
        /// indefinite integral of a polynomial.
        /// </summary>
        /// <param name="a">The lower limit of the integral.</param>
        /// <param name="b">The upper limit of the integral.</param>
        /// <returns>The result of the integral evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialIntegral(double a, double b)
        {

        }
    }
}
