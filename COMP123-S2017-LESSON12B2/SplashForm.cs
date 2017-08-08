using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/* Name: Sylvere Ekponon
 * Date: August 8, 2017
 * Description: Calculator Demo Project
 * Version: 0.1 - Created SplashFormTimer Tick event
 */
namespace COMP123_S2017_LESSON12B2
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for the "Tick"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashFormTimer_Tick(object sender, EventArgs e)
        {
            CalculatorForm calculatorForm = new CalculatorForm();
            calculatorForm.Show();
            this.Hide();

            SplashFormTimer.Enabled = false; //turned timer off
        }
    }
}
