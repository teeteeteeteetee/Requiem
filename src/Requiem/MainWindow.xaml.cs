using Microsoft.Win32;
using Requiem.Account;
using Requiem.Configuration;
using Requiem.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Requiem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /// PUT /rso-auth/v1/session/credentials HTTP/1.1
    /// {
    ///     username: "",
    ///     password: "",
    ///     persistLogin: false,
    /// }


    public partial class MainWindow : Window
    {
        Config? config = new Config();

        public MainWindow()
        {
            InitializeComponent();
        }

        public string ExtractArgument(string CommandArguments, string argToExtract)
        {
            var args = CommandArguments.Split(' ');
            foreach (var item in args)
            {
                if (item.Contains(argToExtract))
                {
                    return item.Replace(argToExtract, "");
                }
            }

            return String.Empty;
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!config.Validate())
            {
                MessageBox.Show("Couldn't find RiotClientServices.exe");

                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Riot Client Services (RiotClientServices.exe)|RiotClientServices.exe";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() is true)
                {
                    config.SetPath(openFileDialog.FileName);
                }

            }
            Login();
        }

        public void Login()
        {
            int count = 0;
            var riotClientServices = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = config!.GetPath()
                }
            };

            riotClientServices.Start();
            while (Process.GetProcessesByName("RiotClientUx") is null)
            {
                if (count >= 30)
                {
                    riotClientServices.Close();
                    MessageBox.Show("Couldn't start Riot Client");
                    Debug.WriteLine("Coulnd't open Riot Client");
                    return;
                }
                count++;
                Debug.WriteLine($"Attempt: {count}");
                Thread.Sleep(10);
            }

            string query = @$"SELECT CommandLine FROM Win32_Process WHERE Name = 'RiotClientUx.exe'";
            string? commandArgument = "";
            string riotClientPort;
            string riotClientPassword;

            while (commandArgument.Equals(""))
            {
                using (var searcher = new ManagementObjectSearcher(query))
                using (var collection = searcher.Get())
                {
                    foreach (var item in collection)
                    {
                        if (item is not null)
                        {
                            commandArgument = item["CommandLine"].ToString();
                        }
                    }
                }
                Thread.Sleep(100);
            }
            riotClientPort = ExtractArgument(commandArgument, "--app-port=");
            riotClientPassword = ExtractArgument(commandArgument, "--remoting-auth-token=");
            

            var json = new LoginTemplate()
            {
                username = "",
                password = "",
                persistLogin = false,
            };

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => {
                return true;
            };

            var client = new HttpClient(handler);
            var byteArray = Encoding.ASCII.GetBytes($"riot:{riotClientPassword}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var task = Task.Run(() => client.PutAsJsonAsync<LoginTemplate>($@"https://127.0.0.1:{riotClientPort}/rso-auth/v1/session/credentials", json));
            task.Wait();
            var response = task.Result;
            Debug.WriteLine(response);
        }
    }
}
