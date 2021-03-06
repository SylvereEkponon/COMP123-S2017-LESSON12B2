﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* Name: Sylvere Ekponon
 * Student ID: 300936445
 * Date: August 3, 2017
 * Description: Calculator Demo Project
 * Version: 1.6 - Added the backspace operator
 */

namespace COMP123_S2017_LESSON12B2
{
    public partial class CalculatorForm : Form
    {
        //PRIVATE INSTANCE VARIABLES
        private bool _isDecimalClicked;

        private string _currentOperator;

        private List<double> _operandList;

        private double _result;

        private bool _isOperandTwo;

        //PUBLIC PROPERTIES
        public bool IsDecimalClicked
        {
            get
            {
                return this._isDecimalClicked;
            }
            set
            {
                this._isDecimalClicked = value;
            }
        }

        public string CurrentOperator
        {
            get
            {
                return this._currentOperator;
            }
            set
            {
                this._currentOperator = value;
            }
        }

        public List<double> OperandList
        {
            get
            {
                return this._operandList;
            }
            set
            {
                this._operandList = value;
            }
        }

        public double Result
        {
            get
            {
                return this._result;
            }

            set
            {
                this._result = value;
            }
        }

        public bool IsOperandTwo
        {
            get
            {
                return this._isOperandTwo;
            }
            set
            {
                this._isOperandTwo = value;
            }
        }


        //CONSTRUCTORS
    
        /// <summary>
        /// This is the main constructor for the CalculatorForm class
        /// </summary>        
        public CalculatorForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This is an event handler for the formclosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// This is a shared event handler for the Calculator button
        /// Not including the Operator Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            Button calculatorButton = sender as Button; //Downcasting

            if ((this.IsDecimalClicked)&&(calculatorButton.Text=="."))
            {
                return;
            }

            if (calculatorButton.Text==".")
            {
                this.IsDecimalClicked = true;
            }

            if (ResultTextBox.Text=="0")
            {
                if (calculatorButton.Text==".")
                {
                    ResultTextBox.Text += calculatorButton.Text;
                }
                else
                {
                    ResultTextBox.Text = calculatorButton.Text;
                }
            }

            else
            {
                if ((OperandList.Count > 0)&&(this.IsOperandTwo==false))
                {
                    ResultTextBox.Text = calculatorButton.Text;
                    this.IsOperandTwo = true;
                }
                else
                {
                    ResultTextBox.Text += calculatorButton.Text;
                }
                
            }

            
        }
        /// <summary>
        /// This is a shared event handler for the Operator button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OperatorButton_Click(object sender,EventArgs e)
        {
            Button operatorButton = sender as Button; //downcasting

            double operand = this._convertOperand(ResultTextBox.Text);

            switch (operatorButton.Text)
            {
                case "C":
                    this._clear();
                    break;

                case "=":
                    this._showResult(operand);
                    break;

                case "◄":
                    this._correction();
                    break;

                case "±":
                    break;

                default:
                    this._calculate(operand, operatorButton.Text);
                    break;
            }

        }
        /// <summary>
        /// This is the _correction (backspace) method
        /// </summary>
        private void _correction()
        { int length = ResultTextBox.TextLength;
            if (length>0)
            {
                ResultTextBox.Text = ResultTextBox.Text.Remove(length - 1, 1);
            }

        }

        /// <summary>
        /// This methods shows the result of all the operands in the OperandList
        /// </summary>
        /// <param name="text"></param>
        private void _showResult(double operand)
        {
            this._calculate(operand, this.CurrentOperator);
            ResultTextBox.Text = this.Result.ToString();

        }

        /// <summary>
        /// This method calculates the result of all the operands in the OperandList
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        private void _calculate(double operand, string operatorString)
        {

            OperandList.Add(operand);
            if (OperandList.Count>1)
            {
                switch (operatorString)
                {
                    case "+":
                        this.Result = this.OperandList[0] + this.OperandList[1];
                        break;

                    case "-":
                        this.Result = this.OperandList[0] - this.OperandList[1];
                        break;

                    case "x":
                        this.Result = this.OperandList[0] * this.OperandList[1];
                        break;

                    case "÷":
                        if (this.OperandList[1]!=0)
                        {
                            this.Result = this.OperandList[0] / this.OperandList[1];
                        }
                        else
                        {

                          this.ResultTextBox.Text = "Cannot by divide by zero";
                        }
                        
                        break;
                }
                this.OperandList.Clear();
                this.OperandList.Add(this.Result);
                this.IsOperandTwo = false;
            }
            this.CurrentOperator = operatorString;
        }

        /// <summary>
        /// This method converts the string from the resultTextBox to a number
        /// </summary>
        /// <param name="operandString"></param>
        /// <returns></returns>
        private double _convertOperand(string operandString)
        {
            try
            {
                return Convert.ToDouble(operandString);
            }
            catch (Exception exception)
            {

                Debug.WriteLine("An Error Occured");
                Debug.WriteLine(exception.Message);
            }
            return 0;
            
        }

        /// <summary>
        /// This is the private clear method
        /// It resets/clears the calculator
        /// </summary>
        private void _clear()
        {
            this.IsDecimalClicked = false;
            ResultTextBox.Text = "0";
            this.CurrentOperator = "C";
            this.OperandList = new List<double>();
            this.IsOperandTwo = false;
            this.Result = 0;


        }



        /// <summary>
        /// This is the event handler for the "Load" event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            this._clear();

        }
    }
}
