using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sumo.Ui.Views
{
    public partial class GroupTeamForm : Form
    {
        private List<ListView> groupList;
        static Random rnd = new Random();
        public GroupTeamForm()
        {
            Thread splashThread = new Thread(new ThreadStart(StartSplash));
            splashThread.Start();
            Thread.Sleep(3000);
            splashThread.Abort();
            InitializeComponent();
            AddGroupToList();
            StartServer();
        }
        public void StartSplash()
        {
            Application.Run(new SplashScreen());
        }

        private void AddGroupToList()
        {
            groupList = new List<ListView>();
            groupA.View = View.List;
            groupB.View = View.List;
            groupC.View = View.List;
            groupD.View = View.List;
            groupList.Add(groupA);
            groupList.Add(groupB);
            groupList.Add(groupC);
            groupList.Add(groupD);
        }

        public void StartServer()
        {
            CreateServer();
        }


        private void CreateServer()
        {
            Int32 port = 2002;

            IPAddress ip = IPAddress.Parse("127.0.0.1");

            TcpListener tcp = new TcpListener(ip, port);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        var teamCode = incomming;
                        //add the team to group
                        AddTeamToGroup(teamCode);
                        tcpClient.Close();
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        private void AddTeamToGroup(string teamCode)
        {
            var team = Controls.Find(teamCode, true).FirstOrDefault() as Button;
            if (team != null)
            {
                int r = rnd.Next(groupList.Count);
                if (groupList[r].Items.Count < 4)
                {
                    groupList[r].Invoke((MethodInvoker)(() => groupList[r].Items.Add(team.Text)));
                    team?.Invoke((MethodInvoker)(() => team.Enabled = false));
                }
                else if (groupList[r].Items.Count == 4)
                {
                    var nonFilledGroup = groupList.FirstOrDefault(x => x.Items.Count < 4);
                    if (nonFilledGroup == null)
                    {
                        if (groupList[0].Items.Count != 5)
                        {
                            groupList[0]?.Invoke((MethodInvoker)(() => groupList[0].Items.Add(team.Text)));
                        }
                        else if (groupList[1].Items.Count != 5)
                        {
                            groupList[1]?.Invoke((MethodInvoker)(() => groupList[1].Items.Add(team.Text)));
                        }
                        team?.Invoke((MethodInvoker)(() => team.Enabled = false));


                    }
                    else
                    {
                        nonFilledGroup?.Invoke((MethodInvoker)(() => nonFilledGroup.Items.Add(team.Text)));
                        team?.Invoke((MethodInvoker)(() => team.Enabled = false));
                    }


                }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            var allButtons = Controls.OfType<Button>();
            var teamButtons = allButtons.Where(x => x.Name != "start");
            foreach (var team in teamButtons)
            {
                int r = rnd.Next(groupList.Count);
                if (groupList[r].Items.Count < 4)
                {
                    groupList[r].Invoke((MethodInvoker)(() => groupList[r].Items.Add(team.Text)));
                    team?.Invoke((MethodInvoker)(() => team.Enabled = false));
                }
                else if (groupList[r].Items.Count == 4 || groupList[r].Items.Count == 5)
                {
                    var nonFilledGroup = groupList.FirstOrDefault(x => x.Items.Count < 4);
                    if (nonFilledGroup == null)
                    {
                        if (groupList[0].Items.Count != 5 || groupList[1].Items.Count != 5)
                        {
                            if (groupList[0].Items.Count != 5)
                            {
                                groupList[0]?.Invoke((MethodInvoker)(() => groupList[0].Items.Add(team.Text)));
                            }
                            else
                            {
                                groupList[1]?.Invoke((MethodInvoker)(() => groupList[1].Items.Add(team.Text)));
                            }
                            
                        }
                        team?.Invoke((MethodInvoker)(() => team.Enabled = false));


                    }
                    else
                    {
                        nonFilledGroup?.Invoke((MethodInvoker)(() => nonFilledGroup.Items.Add(team.Text)));
                        team?.Invoke((MethodInvoker)(() => team.Enabled = false));
                    }
                }

            }


        }
    }
}
