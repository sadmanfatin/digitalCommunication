using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace digitalCommunication
{
    public partial class MARKER__RATIO : Form
    {
        public MARKER__RATIO()
        {
            InitializeComponent();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var m = new FRE_POP_UP();
            m.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var m = new FabricRetun();
            m.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var m = new diaUnitBPO();
            m.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var m = new PopulateFabric();
            m.Show();
        }


    }
}
