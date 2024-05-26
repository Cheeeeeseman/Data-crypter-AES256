using swBootloader;
using swCrypter;
using System.Security.Cryptography;
using System.Text;

namespace swLocker
{
    public partial class Form1 : Form
    {
        //----------------------------------------------------------------------------------------
        string sourceFileName = "";             // ����, ������� ����� ���������
        string outputFileName = "";             // ����, ������� ����� ��������� ������������� ������
        string key = "Key";                     // ���� ��� ���������� AES
        string ivSecret = "test";               // ������ ������������� AES
        //----------------------------------------------------------------------------------------
        /**@brief ��������� ����� */
        private static byte[] GetIV(string ivSecret)
        {
            byte[] cryptIV = new byte[16];
            using MD5 md5 = MD5.Create();
            cryptIV = md5.ComputeHash(Encoding.UTF8.GetBytes(ivSecret));
            // �������� ������� ���� � ������������ � ���������� �����������
            //for (int i = 0; i < 4; i++)
            //{
            //    Array.Reverse(cryptIV, i * 4, 4);
            //}
            return cryptIV;
        }

        /**@brief ��������� ������� IV */
        private static byte[] GetKey(string key)
        {
            byte[] cryptKey = new byte[32];
            using SHA256 sha256 = SHA256.Create();
            cryptKey = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            // �������� ������� ���� � ������������ � ���������� �����������
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
            logTextBox.Text = "";  // ������� ���� ����� � logTexBox
            progressBar.Value = 0; // ������� progressBar
            _encryptOpenFileDialog.InitialDirectory = Configurator.settings.sourceFileName; 
            if (_encryptOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceFileName = _encryptOpenFileDialog.FileName;
                if (sourceFileName != null)
                {
                    //log
                    SendLog("-������ ���� ��������");
                    SendLog("-����� �����:");
                    SendLog("-" + sourceFileName);
                    // ������ ���������� ����� � ���������� ��� � ����, ��������� � sourceFileName
                    outputFileName = Path.ChangeExtension(sourceFileName, "enc");
                    // ������������ ������ "�����������" ����� ������ �����
                    Btn_LockSW.Enabled = true;
                    // ���������� ����� ���� 
                    Configurator.SaveSettingsToJSON(AppConsts.JSON_PATH); 
                }
            }
        }

        private void Btn_LockSW_Click(object sender, EventArgs e)
        {
            using Aes aes = Aes.Create();

            // ��������� AES
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.IV =  GetIV(ivSecret);
            aes.Key = GetKey(key);

            // ������ ����� AES ������ ���� ������ 16  ������). ���� ���, �� � ����� ���������� 0xFF
            using FileStream Stream = new FileStream(sourceFileName, FileMode.Append);   //������� �������� ����� �� ���������� ����������
            long ostatok = Stream.Length % AppConsts.PACKET_BLOCK_MAX_SIZE;
            if (ostatok != 0)
            {
                for (int i = 0; i < AppConsts.PACKET_BLOCK_MAX_SIZE - ostatok; i++) { Stream.WriteByte(0xFF); }
            }
            Stream.Close();

            using FileStream inStream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read);     //������� �������� ����� �� ������
            using FileStream outStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write); //������� �������� ����� �� ������
            CryptoStream encStream = new CryptoStream(outStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write); //����� ��� ���������� ������

            long sizeTotal = inStream.Length;   // ������ ������ ��������
            long readTotal = 0;                 // ������� ����� ���������
            int len = 0;                            // ������ �������� ������������ �����
            byte[] bin = new byte[AppConsts.PACKET_DATA_MAX_SIZE];    // ��������� ��������� ��� ������������� ����������
            while (readTotal < sizeTotal)
            {
                len = inStream.Read(bin, 0, 16);
                encStream.Write(bin, 0, len);
                readTotal += len;
            }
            SendLog("-�������� �����������");
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
