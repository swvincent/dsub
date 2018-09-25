////////////////////////////////////////////////////////////////////////////////
/// Validata.cs
/// 
/// Contains methods for validating data in controls and reporting issues
/// using an ErrorProvider.  Based on some of my college work.
/// 
/// Change History
/// 
/// Date        Notes
/// ==========  ================================================================
/// 3/27/2012   Created by Scott.
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scooter
{
    public static class Validata
    {

        #region Public Methods

        /// <summary>
        /// Verify the textbox contains a value.
        /// </summary>
        /// <param name="t">Textbox to test</param>
        /// <param name="e">Error provider to use</param>
        /// <param name="fieldName">User friendly name of field</param>
        /// <returns>True if textbox contains a value, false otherwise.</returns>
        public static bool ContainsValue(TextBox t, ErrorProvider err, string fieldName)
        {
            //Note: In .Net Framework 3.5 and earlier, IsNullOrWhiteSpace is not included so you must do this instead:
            //if (String.IsNullOrEmpty(t.Text) || (t.Text.Trim().Length == 0))
            
            if (String.IsNullOrWhiteSpace(t.Text))
            {
                //Textbox is empty
                String message = fieldName + " must contain a value.";
                err.SetError(t, message);
                return false;
            }
            else
            {
                //Textbox contains value
                err.SetError(t, "");
                return true;
            }
        }


        /// <summary>
        /// Check if textbox contains an integer value.
        /// </summary>
        /// <param name="t">TextBox</param>
        /// <param name="err">ErrorProvider</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>True if TextBox text parses as int, false otherwise</returns>
        public static bool ContainsInteger(TextBox t, ErrorProvider err, string fieldName)
        {
            try
            {
                int i = int.Parse(t.Text);
                return true;
            }
            catch (FormatException)
            {
                //Value is not a valid int.
                String message = fieldName + " must contain an integer value.";
                err.SetError(t, message);
                return false;
            }
        }


        /// <summary>
        /// Check if textbox contains an integer value and >= a given value.
        /// </summary>
        /// <param name="t">TextBox</param>
        /// <param name="err">ErrorProvider</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="lowerBound">lower boundry</param>
        /// <returns>True if TextBox text parses as int and is >= lowerBound, false otherwise</returns>
        public static bool ContainsInteger(TextBox t, ErrorProvider err, string fieldName, int lowerBound)
        {
            try
            {
                int i = int.Parse(t.Text);
                
                if (i >= lowerBound)
                {
                    //Ok
                    err.SetError(t, "");
                    return true;
                }
                else
                {
                    //Value is out of bounds
                    string message = fieldName + " must be a value greater than or equal to " + lowerBound.ToString() + ".";
                    err.SetError(t, message);
                    return false;
                }
            }
            catch (FormatException)
            {
                //Value is not a valid int.
                String message = fieldName + " must contain an integer value.";
                err.SetError(t, message);
                return false;
            }
        }


        /// <summary>
        /// Check if textbox contains an integer value and >= a given value.  No error provider.
        /// </summary>
        /// <param name="t">TextBox</param>
        /// <param name="err">ErrorProvider</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="lowerBound">lower boundry</param>
        /// <returns>True if TextBox text parses as int and is >= lowerBound, false otherwise</returns>
        public static bool ContainsInteger(TextBox t, string fieldName, int lowerBound)
        {
            try
            {
                int i = int.Parse(t.Text);

                if (i >= lowerBound)
                {
                    //Ok
                    return true;
                }
                else
                {
                    //Value is out of bounds
                    string message = fieldName + " must be a value greater than or equal to " + lowerBound.ToString() + ".";
                    return false;
                }
            }
            catch (FormatException)
            {
                //Value is not a valid int.
                String message = fieldName + " must contain an integer value.";
                return false;
            }
        }


        /// <summary>
        /// Check if textbox contains an integer value and is within lower and upper boundries.
        /// </summary>
        /// <param name="t">TextBox</param>
        /// <param name="err">ErrorProvider</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="lowerBound">lower boundry</param>
        /// /// <param name="upperBound">upper boundry</param>
        /// <returns>True if TextBox text parses as int and is within bounds, false otherwise</returns>
        public static bool ContainsInteger(TextBox t, ErrorProvider err, string fieldName, int lowerBound, int upperBound)
        {
            try
            {
                int i = int.Parse(t.Text);

                if (i >= lowerBound && i <= upperBound)
                {
                    //Ok
                    err.SetError(t, "");
                    return true;
                }
                else
                {
                    //Value is out of bounds
                    string message = fieldName + " must be a value between " + lowerBound.ToString() + " and " +
                        upperBound.ToString() + ".";
                    err.SetError(t, message);
                    return false;
                }
            }
            catch (FormatException)
            {
                //Value is not a valid int.
                String message = fieldName + " must contain an integer value.";
                err.SetError(t, message);
                return false;
            }
        }


        /// <summary>
        /// Check if textbox contains a double value.
        /// </summary>
        /// <param name="t">TextBox</param>
        /// <param name="err">ErrorProvider</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>True if TextBox text parses as int, false otherwise</returns>
        public static bool ContainsDouble(TextBox t, ErrorProvider err, string fieldName)
        {
            try
            {
                double d = double.Parse(t.Text);
                return true;
            }
            catch (FormatException)
            {
                //Value is not a valid double.
                String message = fieldName + " must contain a valid numeric value.";
                err.SetError(t, message);
                return false;
            }
        }


        /// <summary>
        /// Check if ComboBox contains a value.
        /// </summary>
        /// <param name="c">ComboBox RadDropDownList</param>
        /// <param name="err">ErrorProvider</param>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True if value is selected in ComboBox, false otherwise</returns>
        public static bool ValueSelected(ComboBox c, ErrorProvider err, string fieldName)
        {
            if (c.SelectedIndex != -1)
            {
                //Ok
                err.SetError(c, "");
                return true;
            }
            else
            {
                //Missing value
                string message = fieldName + " is a required field.";
                err.SetError(c, message);
                return false;
            }
        }

        #endregion Public Methods

    }
}