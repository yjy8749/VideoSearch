using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    public sealed partial class ExploreForm : Form
    {
        private ExploreForm()
        {
            InitializeComponent();
        }
        private static readonly ExploreForm INTERFACE = new ExploreForm();
        public static ExploreForm getInterface()
        {
            return ExploreForm.INTERFACE;
        }

    }
}
