using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal class ColorWorker
    {
        #region Variables

        private string mColorMode { get; set; }

        #endregion
        #region Constructor

        internal ColorWorker(string colorMode)
        {
            mColorMode = colorMode;
        }

        #endregion
        #region Methods

        internal void ChangeDataGridViewColors(DataGridView dgv)
        {
            Color backColor = Color.White;
            Color foreColor = Color.Black;
            if (mColorMode == Form1.mColorModesEnum.Dark.ToString())
            {
                backColor = Color.DarkGray;
                foreColor = Color.White;
            } // if
            else if (mColorMode == Form1.mColorModesEnum.System.ToString())
            {
                backColor = Color.White;
                foreColor = Color.Black;
            } // else if

            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.DefaultCellStyle.BackColor = backColor;
                row.DefaultCellStyle.ForeColor = foreColor;
            }
        }

        internal void ChangeTextBoxColors(Control control)
        {
            if (mColorMode == Form1.mColorModesEnum.Dark.ToString())
            {
                control.BackColor = Color.DarkGray;
                control.ForeColor = Color.White;
            } // if
            else if (mColorMode == Form1.mColorModesEnum.System.ToString())
            {
                control.BackColor = Color.White;
                control.ForeColor = Color.Black;
            } // else if
        }

        internal void ChangeLabelColors(Control control)
        {
            if (mColorMode == Form1.mColorModesEnum.Dark.ToString())
            {
                control.ForeColor = Color.DarkGray;
            } // if
            else if (mColorMode == Form1.mColorModesEnum.System.ToString())
            {
                control.ForeColor = Color.Black;
            } // else if
        }

        internal void ChangeButtonColors(Button button)
        {
            if (mColorMode == Form1.mColorModesEnum.Dark.ToString())
            {
                button.BackColor = Color.Black;
                button.ForeColor = Color.DarkGray;
            } // if
            else if (mColorMode == Form1.mColorModesEnum.System.ToString())
            {
                button.BackColor = Color.FromArgb(0, 255, 255, 255);
                button.ForeColor = Color.Black;
            } // else if
        }

        #endregion
    }
}
