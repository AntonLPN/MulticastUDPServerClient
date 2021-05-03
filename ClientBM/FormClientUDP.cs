using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary_UDP_Sender;


//Задание №1
//Создайте систему рассылки информационных сообщений внутри компании. 
//Система состоит из двух приложений. Клиентское приложение используется для 
//получения сообщений. Серверное приложение позволяет ввести и разослать 
//сообщение. Проектируйте сетевую архитектуру системы таким образом, чтобы она 
//не зависела от вида приложения и была легко переносима в другой вид 
//приложения.
namespace ClientBM
{
    public partial class FormClientUDP : Form
    {

       
        ClientUDP clientUDP;


        private string Ip;
        private int Port;
        public FormClientUDP()
        {
            InitializeComponent();
            clientUDP = new ClientUDP();
            Ip = string.Empty;
            Port = 0;
        }

        private void AddText(string str)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(str);
            textBox1.Text += builder.ToString() + Environment.NewLine;
        }

     

        private async void buttonRecive_Click(object sender, System.EventArgs e)
        {
            

            if (!string.IsNullOrEmpty(textBoxIP.Text) && !string.IsNullOrEmpty(textBoxPort.Text))
            {
                Ip = textBoxIP.Text;
                //проверка что порт введен коректно
                try
                {
                    Port = Convert.ToInt32(textBoxPort.Text);
                }
                catch (FormatException ex)
                {

                    MessageBox.Show(ex.Message, "MyError 01", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //если с портом все ок
                if (Port != 0 && Ip != " ")
                {
                   


                    Task<string> task1 = new Task<string>(() => clientUDP.Listener(Ip, Port));
                    task1.Start();
                    await task1;
                    string result = task1.Result;
                    
                    textBox1.Text += result+Environment.NewLine;


                }
                else
                {
                    MessageBox.Show("Что то пошло не так с входящими данными", "MyError02", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



        }
    }
}
