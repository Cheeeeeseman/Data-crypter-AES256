using swBootloader;
using swCrypter;
using System.Security.Cryptography;
using System.Text;

namespace swLocker
{
    public partial class Form1 : Form
    {
        //----------------------------------------------------------------------------------------
        string sourceFileName = "";             // Файл, который будем шифровать
        string outputFileName = "";             // Файл, который будет содержать зашифрованные данные
        string key = "Key";                     // Ключ для шифрования AES
        string ivSecret = "test";               // Вектор инициализации AES
        //----------------------------------------------------------------------------------------
        /**@brief получение ключа */
        private static byte[] GetIV(string ivSecret)
        {
            byte[] cryptIV = new byte[16];
            using MD5 md5 = MD5.Create();
            cryptIV = md5.ComputeHash(Encoding.UTF8.GetBytes(ivSecret));
            // Изменяем порядок байт в соответствии с алгоритмом контроллера
            //for (int i = 0; i < 4; i++)
            //{
            //    Array.Reverse(cryptIV, i * 4, 4);
            //}
            return cryptIV;
        }

        /**@brief получение вектора IV */
        private static byte[] GetKey(string key)
        {
            byte[] cryptKey = new byte[32];
            using SHA256 sha256 = SHA256.Create();
            cryptKey = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            // Изменяем порядок байт в соответствии с алгоритмом контроллера
            for (int i = 0; i < 8; i++)
            {
                Array.Reverse(cryptKey, i * 4, 4);
            }
            return cryptKey;
        }
        //----------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_ChooseSW_MouseClick(object sender, MouseEventArgs e)
        {
            logTextBox.Text = "";  // Стираем весь текст в logTexBox
            progressBar.Value = 0; // Стираем progressBar
            _encryptOpenFileDialog.InitialDirectory = Configurator.settings.sourceFileName; 
            if (_encryptOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceFileName = _encryptOpenFileDialog.FileName;
                if (sourceFileName != null)
                {
                    //log
                    SendLog("-Открыт файл прошивки");
                    SendLog("-Адрес Файла:");
                    SendLog("-" + sourceFileName);
                    // Меняем расширение файла и записываем уго в путь, указанный в sourceFileName
                    outputFileName = Path.ChangeExtension(sourceFileName, "enc");
                    // Разблокируем кнопку "Зашифровать" после выбора файла
                    Btn_LockSW.Enabled = true;
                    // Подсейвили новый путь 
                    Configurator.SaveSettingsToJSON(AppConsts.JSON_PATH); 
                }
            }
        }

        private void Btn_LockSW_Click(object sender, EventArgs e)
        {
            using Aes aes = Aes.Create();

            // настройка AES
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.IV =  GetIV(ivSecret);
            aes.Key = GetKey(key);

            // Размер блока AES должен быть кратен 16  байтам). Если нет, то в конце дописываем 0xFF
            using FileStream Stream = new FileStream(sourceFileName, FileMode.Append);   //создаем файловый поток на добавление информации
            long ostatok = Stream.Length % AppConsts.PACKET_BLOCK_MAX_SIZE;
            if (ostatok != 0)
            {
                for (int i = 0; i < AppConsts.PACKET_BLOCK_MAX_SIZE - ostatok; i++) { Stream.WriteByte(0xFF); }
            }
            Stream.Close();

            using FileStream inStream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read);     //создаем файловый поток на чтение
            using FileStream outStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write); //создаем файловый поток на запись
            CryptoStream encStream = new CryptoStream(outStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write); //поток для шифрования данных

            long sizeTotal = inStream.Length;   // Полный размер прошивки
            long readTotal = 0;                 // Сколько всего прочитано
            int len = 0;                            // размер текущего прочитанного куска
            byte[] bin = new byte[AppConsts.PACKET_DATA_MAX_SIZE];    // Временное Хранилище для зашифрованной информации
            while (readTotal < sizeTotal)
            {
                len = inStream.Read(bin, 0, 16);
                encStream.Write(bin, 0, len);
                readTotal += len;
            }
            SendLog("-Прошивка зашифрована");
            encStream.Close();
            outStream.Close();
            inStream.Close();
        }

        public void SendLog(string text)
        {
            logTextBox.Text += text + "\r\n";
        }
    }
}
