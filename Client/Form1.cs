using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        string serverAddress = "127.0.0.1";
        int port = 6000;
        TcpClient client = null;
        NetworkStream stream = null;

        public Form1()
        {
            InitializeComponent();
        }

        // Підключення до сервера при завантаженні форми
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = await ConnectToServerAsync(serverAddress, port);
                stream = client.GetStream();
                MessageBox.Show("Connected to server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}");
            }
        }

        // Асинхронне підключення до сервера
        private async Task<TcpClient> ConnectToServerAsync(string serverAddress, int port)
        {
            var client = new TcpClient();
            await client.ConnectAsync(serverAddress, port);
            return client;
        }

        // Відключення від сервера при закритті форми
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            await DisconnectFromServerAsync(client);
            stream?.Close();
        }

        // Асинхронне відключення клієнта
        private async Task DisconnectFromServerAsync(TcpClient client)
        {
            if (client != null && client.Connected)
            {
                client.Close();
                Console.WriteLine("Disconnected from the server.");
            }
        }

        // Кнопка для відправки запиту на сервер
        private async void SendButton_Click(object sender, EventArgs e)
        {
            string currencyCode1 = FirstText.Text.ToUpper();
            string currencyCode2 = SecondText.Text.ToUpper();

            // Перевірка, чи введено коректні значення валют
            if (string.IsNullOrEmpty(currencyCode1) || string.IsNullOrEmpty(currencyCode2))
            {
                MessageBox.Show("Please enter valid currency codes.");
                return;
            }

            try
            {
                // Перевірка, чи з’єднання ще активне
                if (client == null || !client.Connected)
                {
                    MessageBox.Show("Client is not connected to the server.");
                    return;
                }

                await SendCurrencyCodesAsync(stream, currencyCode1, currencyCode2);

                // Отримуємо результат ділення курсів
                decimal result = await ReceiveDivisionResultAsync(stream);

                // Відображаємо результат на лейблі
                ResultLabel.Text = $"Result: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Асинхронна відправка кодів валют
        private async Task SendCurrencyCodesAsync(NetworkStream stream, string currencyCode1, string currencyCode2)
        {
            await SendStringAsync(stream, currencyCode1);
            await SendStringAsync(stream, currencyCode2);
        }

        // Асинхронне отримання результату ділення курсів
        private async Task<decimal> ReceiveDivisionResultAsync(NetworkStream stream)
        {
            int resultLength = await ReadIntAsync(stream);
            string resultString = await ReadStringAsync(stream, resultLength);
            return decimal.Parse(resultString);
        }

        // Асинхронна відправка рядка
        private async Task SendStringAsync(NetworkStream stream, string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(BitConverter.GetBytes(messageBytes.Length), 0, 4); // Відправляємо довжину
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);             // Відправляємо сам рядок
        }

        // Асинхронне зчитування цілих чисел
        private async Task<int> ReadIntAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[4];
            await stream.ReadAsync(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        // Асинхронне зчитування рядка
        private async Task<string> ReadStringAsync(NetworkStream stream, int length)
        {
            byte[] buffer = new byte[length];
            await stream.ReadAsync(buffer, 0, length);
            return Encoding.ASCII.GetString(buffer);
        }
    }
}
