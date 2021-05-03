using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary_UDP_Sender;

//Тема: Unicast, broadcast, multicast.
//Задание №1
//Создайте систему рассылки информационных сообщений внутри компании. 
//Система состоит из двух приложений. Клиентское приложение используется для 
//получения сообщений. Серверное приложение позволяет ввести и разослать 
//сообщение. Проектируйте сетевую архитектуру системы таким образом, чтобы она 
//не зависела от вида приложения и была легко переносима в другой вид 
//приложения.
namespace ServerBM
{
    public partial class ServerCompanyForm : Form
    {
        private string Ip;
        private int Port;
        private ServerUDP serverUDP;//из моей библиотеки


        public ServerCompanyForm()
        {
            InitializeComponent();
            Ip = string.Empty;
            Port = 0;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //Проверка на пустоту введеных данных
            if (!string.IsNullOrEmpty(textBoxIP.Text) && !string.IsNullOrEmpty(textBoxPort.Text) && !string.IsNullOrEmpty(textBoxMessages.Text))
            {
                Ip = textBoxIP.Text;
                //проверка что порт введен коректно
                try
                {
                    Port = Convert.ToInt32(textBoxPort.Text);
                }
                catch (FormatException ex)
                {

                    MessageBox.Show(ex.Message,"MyError 01",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                serverUDP = new ServerUDP();
                //если с портом все ок
                if (Port!=0 && Ip!=" ")
                {
                    serverUDP.SendMessages(Ip, Port, textBoxMessages.Text);

                    textBoxInformation.Text += "Сообщение отправлено"+Environment.NewLine;



                }
                else
                {
                    MessageBox.Show("Что то пошло не так с входящими данными","MyError02",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Ошибка заполните все поля","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
