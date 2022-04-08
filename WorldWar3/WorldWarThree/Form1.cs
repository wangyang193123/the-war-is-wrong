using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;


namespace WorldWarThree
{
    public partial class Form1 : Form
    {
        bool AL = false;
        bool RL = false;
        Thread threadA ;
        Thread threadR ;
        Thread threadU;

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
            threadA = new Thread(Alight);
            threadA.Start();
            threadR = new Thread(Rlight);
            threadR.Start();

            threadU = new Thread(BW);
            threadU.Start();

        }

        private void Alight()
        {
            while (true)
            {
                while (AL)
                {
                    pictureBox1.BackColor = Color.Red;
                    pictureBox1.Refresh();
                    Thread.Sleep(500);
                    pictureBox1.BackColor = Color.White;
                    pictureBox1.Refresh();
                    Thread.Sleep(500);

                }
            }

         }

        private void Rlight()
        {
            while (true)
            {
                while (RL)
                {
                    pictureBox2.BackColor = Color.Red;
                    pictureBox2.Refresh();
                    Thread.Sleep(500);
                    pictureBox2.BackColor = Color.White;
                    pictureBox2.Refresh();
                    Thread.Sleep(500);


                }
            }
        }

        private void BW()
        {
            while (true)
            {
                try
                {

                    Int64 int_rec1 = 0;
                    Int64 int_spe1 = 0;
                    Int64 int_balance1 = 0;
                    using (WebClient webClient = new WebClient())
                    {


                        string res = webClient.DownloadString(
                             $"https://blockstream.info/api/address/1USAsHnURGSWxJp5F7yw4XGx7CmmdytYo");

                        var jsonObj = (JsonObject)JsonConvert.AnalysisJson(res);

                        foreach (var item in jsonObj)
                        {



                            if (item.Key == "chain_stats")
                            {

                                var jsonObj1 = (JsonObject)JsonConvert.AnalysisJson(item.Value.ToString());
                                foreach (var item1 in jsonObj1)

                                {
                                    if (item1.Key == "funded_txo_sum")
                                    {
                                        int_rec1 = Convert.ToInt64(item1.Value.ToString());
                                    }
                                    if (item1.Key == "spent_txo_sum")
                                    {
                                        int_spe1 = Convert.ToInt64(item1.Value.ToString());
                                    }

                                }
                                int_balance1 = int_rec1;

                                Avnumtxt.Text = (int_balance1 / 1000).ToString();
                            }

                        }

                    }

                    Thread.Sleep(1000);


                    Int64 int_rec8 = 0;
                    Int64 int_spe8 = 0;
                    Int64 int_balance8 = 0;

                    using (WebClient webClient = new WebClient())
                    {


                        string res = webClient.DownloadString(
                             $"https://blockstream.info/api/address/1RUSzaRGiQYuRXbJV8cqekc6CwyjuWM6q");

                        var jsonObj = (JsonObject)JsonConvert.AnalysisJson(res);

                        foreach (var item in jsonObj)
                        {

                            if (item.Key == "chain_stats")
                            {

                                var jsonObj1 = (JsonObject)JsonConvert.AnalysisJson(item.Value.ToString());
                                foreach (var item1 in jsonObj1)

                                {
                                    if (item1.Key == "funded_txo_sum")
                                    {
                                        int_rec8 = Convert.ToInt64(item1.Value.ToString());
                                    }
                                    if (item1.Key == "spent_txo_sum")
                                    {
                                        int_spe8 = Convert.ToInt64(item1.Value.ToString());
                                    }

                                }
                                int_balance8 = int_rec8;

                                Rvnumtxt.Text = (int_balance8 / 1000).ToString();
                            }

                        }

                    }

                    if (int_balance1 > int_balance8)
                    {
                        RL = true;
                        AL = false;
                        label4.Refresh();
                        label5.Refresh();
                        label4.Visible = false;
                        label5.Visible = true;

                    }

                    else if (int_balance1 < int_balance8)
                    {
                        RL = false;
                        AL = true;
                        label4.Refresh();
                        label5.Refresh();
                        label4.Visible = true;
                        label5.Visible = false;
                    }

                    if ((int_balance1 != 0) && (int_balance8 != 0) && (int_balance1 == int_balance8))
                    {
                        RL = false;
                        AL = false;
                        label4.Refresh();
                        label5.Refresh();
                        label4.Visible = true;
                        label5.Visible = true;
                    }

                    if (int_balance1 == 0 && int_balance8 == 0)
                    {
                        label4.Refresh();
                        label5.Refresh();
                        label4.Visible = false;
                        label5.Visible = false;

                    }


                    Thread.Sleep(1000);

                }
                catch
                {

                    Thread.Sleep(1000);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //Task.Factory.StartNew(() => BW());
        }

        private void Form_Closed(object sender, EventArgs e)
        {
            threadA.Abort();
            threadR.Abort();
            threadU.Abort();
            System.Environment.Exit(0);

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
