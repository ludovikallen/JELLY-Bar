///////////////////////////////////////////////////////////////////////////////////
// Author : Nicolas Chourot
// September 2017
///////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashButton
{
    [DefaultEvent("Click")]
    public partial class FlashButton : UserControl
    {
        public FlashButton()
        {
            InitializeComponent();
        }

        public Image ImageNeutral { set; get; }
        public Image ImageOver    { set; get; }
        public Image ImageClick   { set; get; }
        public Image ImageDisable { set; get; }

        private void FlashButton_Load(object sender, EventArgs e)
        {
            if (ImageNeutral != null)
                this.BackgroundImage = ImageNeutral;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FlashButton_MouseEnter(object sender, EventArgs e)
        {
            if (ImageOver != null)
                this.BackgroundImage = ImageOver;
        }

        private void FlashButton_MouseLeave(object sender, EventArgs e)
        {
            if (ImageNeutral != null)
                this.BackgroundImage = ImageNeutral;
        }

        private void FlashButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (ImageClick != null)
                this.BackgroundImage = ImageClick;
        }

        private void FlashButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (ImageOver != null)
                this.BackgroundImage = ImageNeutral;
        }

        private void FlashButton_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.Enabled)
            {
                if (ImageDisable != null)
                    this.BackgroundImage = ImageDisable;
            }
            else
            {
                if (ImageNeutral != null)
                    this.BackgroundImage = ImageNeutral;
            }
        }
    }
}
