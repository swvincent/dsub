////////////////////////////////////////////////////////////////////////////////
/// WinFormHelper.cs
/// Scott W. Vincent.
/// http://www.swvincent.com
/// 
/// "Helper" code for Window forms, for menus, error handling, etc.
/// 
/// Changed History
/// 
/// Date        Notes
/// ==========  ================================================================
/// 3/6/2012    Created by Scott.
/// 5/16/2012   Various revisions made during NCR project by Scott.
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;       //For List<>

namespace scooter
{
    public static class WinFormHelper
    {

        #region Menu Code

        /// <summary>
        /// Simple undo for TextBoxBase controls.
        /// </summary>
        /// <param name="c">Control to perform undo on.</param>
        /// <returns>True if undo could be run on control.  Does not neccesarily mean undo worked!</returns>
        public static bool SimpleUndo(Control c)
        {
            if (c is TextBoxBase)
            {
                if (((TextBoxBase)c).CanUndo)
                {
                    ((TextBoxBase)c).Undo();
                    return true;
                }
                else
                    //No undo for this control
                    return false;
            }
            else
                //Can't undo for other controls
                return false;
        }

        /// <summary>
        /// Simple redo for RichTextBox controls.
        /// </summary>
        /// <param name="c">Control to perform redo on.</param>
        /// <returns>true if redo command could be run on control.  Does not neccessarily mean redo worked!</returns>
        public static bool SimpleRedo(Control c)
        {
            //This redo only works for RichTextBox controls.  I thought it would work for TextBox as well, but it's not supported.  Possible
            //explanation here: http://stackoverflow.com/questions/434658/textbox-undo-redo-commands.  Didn't find much else.

            if (c is RichTextBox)
            {
                if (((RichTextBox)c).CanRedo)
                {
                    ((RichTextBox)c).Redo();
                    return true;
                }
                else
                {
                    //No redo on this control
                    return false;
                }
            }
            else
                //Can't undo for other controls
                return false;
        }

        /// <summary>
        /// Cut text from given control to clipboard if supported
        /// </summary>
        /// <param name="c">Control to cut from</param>
        /// <returns>True if cut succeded, false otherwise</returns>
        public static bool CutText(Control c)
        {
            if (c is TextBoxBase)
            {
                //TextBox, RichTextBox, etc.  Use built-in function.
                ((TextBoxBase)c).Cut();
                return true;
            }
            else if (c is ComboBox && ((ComboBox)c).DropDownStyle == ComboBoxStyle.DropDown)
            {
                //Combobox with text entry
                Clipboard.SetDataObject(c.Text, true);
                c.Text = "";
                ((ComboBox)c).SelectedIndex = -1;
                return true;
            }
            else
            {
                //Can't cut for other controls
                return false;
            }
        }

        /// <summary>
        /// Copy Text from given control to clipboard if supported
        /// </summary>
        /// <param name="c">Control to copy from</param>
        /// <returns>True if copy succeeded, false otherwise</returns>
        public static bool CopyText(Control c)
        {
            if (c is TextBoxBase)
            {
                //TextBox, RichTextBox, etc.  Use built-in function.
                ((TextBoxBase)c).Copy();
                return true;
            }
            else if (c is ComboBox && ((ComboBox)c).DropDownStyle == ComboBoxStyle.DropDown)
            {
                //Combobox with text entry
                Clipboard.SetDataObject(c.Text, true);
                return true;
            }
            else
            {
                //Can't cut for other controls
                return false;
            }
        }

        /// <summary>
        /// Paste text to given control from clipboard if supported
        /// </summary>
        /// <param name="c">Control to paste to</param>
        /// <returns>True if paste succeeded (unless it's ready only TextBoxBase, in which case true is returned even though paste fails), false otherwise</returns>
        public static bool PasteText(Control c)
        {
            if (Clipboard.ContainsText())
            {
                //Clipboard contains text, proceed.
                if (c is TextBoxBase)
                {
                    //TextBox, RichTextBox, etc.  Use built-in function.  If textbox is ReadOnly, this will return true, however the text
                    //will not be pasted and user will get an exclamation sound as built-in function handles it so I'm not worried about it.
                    ((TextBoxBase)c).Paste();
                    return true;
                }
                else if (c is ComboBox && ((ComboBox)c).DropDownStyle == ComboBoxStyle.DropDown)
                {
                    //Combobox with text entry
                    c.Text = Clipboard.GetText();
                    return true;
                }
                else
                {
                    //Can't paste for other controls
                    return false;
                }
            }
            else
                //Nothing to paste
                return false;
        }

        /// <summary>
        /// Delete text from given control if supported
        /// </summary>
        /// <param name="c">Control to delete text from</param>
        /// <returns>True if delete is successful, false otherwise</returns>
        public static bool DeleteText(Control c)
        {
            if (c is TextBoxBase)
            {
                //TextBox, RichTextBox, etc.
                ((TextBoxBase)c).SelectedText = "";
                return true;
            }
            else if (c is ComboBox && ((ComboBox)c).DropDownStyle == ComboBoxStyle.DropDown)
            {
                //Combobox with text entry
                ((ComboBox)c).SelectedText = "";
                return true;
            }
            else
            {
                //Can't delete for other controls
                return false;
            }
        }

        /// <summary>
        /// Select all text from given control if supported
        /// </summary>
        /// <param name="c">Control to select all text in</param>
        /// <returns>True if select all is success, false otherwise</returns>
        public static bool SelectAllText(Control c)
        {
            if (c is TextBoxBase)
            {
                //TextBox, RichTextBox, etc.  Use built-in function.
                ((TextBoxBase)c).SelectAll();
                return true;
            }
            else
            {
                //Other controls ignored
                return false;
            }
        }

        #endregion

        #region Error Handling

        /// <summary>
        /// Simple error message display.
        /// </summary>
        /// <param name="processDescription">Description of process that error occured in</param>
        /// <param name="caught">Exception that was caught</param>
        public static void DisplayErrorMessage(string processDescription, Exception caught)
        {
            StringBuilder b = new StringBuilder();

            b.Append("An error was caught.  Details:\n\nProcess Desc.: " + processDescription);

            Exception c = caught;

            while (c != null)
            {
                string message = "\n\nError Desc.: " + c.Message + "\n\n" +
                "Error Type:" + c.GetType().ToString() + "\n\n" +
                "Stack Trace:\n" + c.StackTrace;
                b.Append(message);
                c = c.InnerException;
            }

            MessageBox.Show(b.ToString(), "Error Caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion Error Handling

        #region Toggles

        /// <summary>
        /// Toggle Menu Strip's items on/off based on their tag and if currently editing record or not.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="editing"></param>
        public static void ToggleMenuStripItems(MenuStrip m, bool editing)
        {
            //This was somewhat helpful:
            //http://www.codeproject.com/Tips/264690/How-to-iterate-recursive-through-all-menu-items-in

            //First go through top level items
            foreach (ToolStripItem mainItem in m.Items)
            {
                if (mainItem is ToolStripMenuItem)
                {
                    //Next you look at all subitems of top level item
                    foreach (ToolStripItem subItem in ((ToolStripMenuItem)mainItem).DropDownItems)
                    {
                        if (subItem.Tag != null && subItem.Tag.ToString() == "enableEditing")
                            subItem.Enabled = editing;
                        else if (subItem.Tag != null && subItem.Tag.ToString() == "enableNotEditing")
                            subItem.Enabled = !editing;
                    }
                }
            }
        }

        /// <summary>
        /// Toggle Tool Strip's items on/off based on their tag and if currently editing record or not.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="editing"></param>
        public static void ToggleToolStripItems(ToolStrip t, bool editing)
        {
            foreach (ToolStripItem i in t.Items)
            {
                if (i is ToolStripButton)
                {
                    if (i.Tag != null && i.Tag.ToString() == "enableEditing")
                        i.Enabled = editing;
                    else if (i.Tag != null && i.Tag.ToString() == "enableNotEditing")
                        i.Enabled = !editing;
                }
            }
        }

        #endregion Toggles

    }
}