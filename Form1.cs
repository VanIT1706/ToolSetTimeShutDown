using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Timers;

namespace ToolHenGio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadStatusbar();
        }         
        
        StatusBarPanel downtimePanel = new StatusBarPanel();
        StatusBarPanel barPanel = new StatusBarPanel();

        //Hàm Status
        private void LoadStatusbar()
        {
            StatusBar bar = new StatusBar();

            bar.ShowPanels = true;
            bar.Panels.Add(barPanel);
            bar.Panels.Add(downtimePanel);

            downtimePanel.Text = "";
            barPanel.Text = "Watting...";

            this.Controls.Add(bar);
        }

        decimal downTime = 0;
        private void numGiay_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown box = sender as NumericUpDown;
            if (box.Value >= 60)
            {
                numPhut.Value++;
                box.Value = 0;
            }
        }

        private void numPhut_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown box = sender as NumericUpDown;
            if (box.Value >= 60)
            {
                numGio.Value++;
                box.Value = 0;
            }
        }
       
        //Hàm tính toán Downtime
        void calculateDownTime()
        {
            downTime = numGiay.Value + numPhut.Value * 60 + numGio.Value * 60 * 60;
        }

        //shutdown theo command
        void ShutDown(string command)
        {
            System.Diagnostics.Process.Start("shutdown",command);
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {

            calculateDownTime();
            ShutDown("-s -t "+downTime.ToString());
            barPanel.Text = "Shutting down...";
            timer1.Start();

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            //ShutDown("-r -t 5");
            calculateDownTime();
            ShutDown("-r -t "+downTime.ToString());
            barPanel.Text = "Restarting...";
            timer1.Start();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            //hủy lệnh là -a
            ShutDown("-a");
            barPanel.Text = "Watting...";
            downtimePanel.Text = "";
            timer1.Stop();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            downTime--;
            downtimePanel.Text = downTime.ToString(); 
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
