using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace ProcessManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void LoadProcesses()
        {
            listProcesses.Items.Clear();

            Process[] processes = Process.GetProcesses();

            foreach (Process p in processes)
            {
                listProcesses.Items.Add(p.ProcessName);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int interval = int.Parse(txtInterval.Text);
            timer1.Interval = interval;
            timer1.Start();

            LoadProcesses();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void listProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listProcesses.SelectedItem == null) return;

            string name = listProcesses.SelectedItem.ToString();

            Process[] processes = Process.GetProcessesByName(name);

            if (processes.Length > 0)
            {
                Process p = processes[0];

                lblInfo.Text =
                    "ID: " + p.Id + Environment.NewLine +
                    "Start time: " + p.StartTime + Environment.NewLine +
                    "CPU time: " + p.TotalProcessorTime + Environment.NewLine +
                    "Threads: " + p.Threads.Count + Environment.NewLine +
                    "Copies: " + processes.Length;
            }
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            if (listProcesses.SelectedItem == null) return;

            string name = listProcesses.SelectedItem.ToString();
            Process[] processes = Process.GetProcessesByName(name);

            if (processes.Length > 0)
            {
                processes[0].Kill();
                LoadProcesses();
            }
        }

        private void btnNotepad_Click(object sender, EventArgs e)
        {
            Process.Start("notepad");
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            Process.Start("calc");
        }

        private void btnPaint_Click(object sender, EventArgs e)
        {
            Process.Start("mspaint");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Programs (*.exe)|*.exe";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Process.Start(dialog.FileName);
            }
        }
    }
}