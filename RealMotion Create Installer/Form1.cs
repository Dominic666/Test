using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Net;


namespace RealMotion_Create_Installer
{
    public partial class Form1 : Form
    {
        private string Source;
        private string Destination;
        MySqlConnection conn;
        private string myConnectionString = "Server=box352.bluehost.com;Database=blazarga_BlazarGames;User ID=blazarga_Dominic;Password=File66677;";
        private string GetVersion = "SELECT VersionNumber FROM RealMotionInfo ORDER BY ID DESC LIMIT 1";
        List<String> FilesWithChanges = new List<String>();
        string DefaultFileLocation = @"C:\Doms\Software\UE4\Builds\RealMotion\WindowsNoEditor";
        string MasterFilesLocation = @"C:\Doms\Software\UE4\Builds\RealMotion MASTER";
        string ServerFilesLocation = @"C:\Doms\Software\UE4\RealMotion\Saved\StagedBuilds\WindowsServer";
        string ServerTargetLocation = @"\\Domio-pc\WindowsServer";
        float TotalSize = 0;
        List<String> UpdateList = new List<String>();
        
        public Form1()
        {
            InitializeComponent();
            tb_files.Text = DefaultFileLocation;
            tb_MainDir.Text = MasterFilesLocation;

            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(GetVersion, conn);
                lb_version.Text = Convert.ToString(cmd.ExecuteScalar()); ;

                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Server is down");
            }

            
        }

        private void SerDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Source = folderBrowserDialog1.SelectedPath;
                tb_files.Text = Source;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Destination = folderBrowserDialog1.SelectedPath;
            }
        }

        private void bt_create_Click(object sender, EventArgs e)
        {
            CompareFiles();
        }

        private void ReadyToUpload()
        {
            float TempSize = TotalSize;
            string sizestring = "";
            if (TempSize < 10000000)
            {
                if (TempSize < 1000)
                {
                    sizestring = TempSize + " Bytes";
                }
                else
                {
                    sizestring = (TempSize / 1024) + " Kbs";
                }
            }
            else
            {
                sizestring = ((TempSize / 1024)/1024) + " Kbs";
            }
            if (FilesWithChanges.Count > 1)
            {
                sizestring = "Ready to upload: Files: " + (FilesWithChanges.Count - 1).ToString() + " Total Size: " + sizestring;
                DialogResult dialogResult = MessageBox.Show(sizestring, "Upload", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    UpdateSQLAndUpload();
                }
            }
            else
            {
                MessageBox.Show("No new files to update");
            }
        }

        private float GetTotalSize()
        {
            TotalSize = 0;
            for (int i = 0; i < FilesWithChanges.Count - 1; i++)
            {
                FileInfo fi = new FileInfo(FilesWithChanges[i]);
                TotalSize += fi.Length;
            }
            return (float)Math.Round(TotalSize,4);
        }

        private void UpdateSQLAndUpload()
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void SetSQL()
        {
            string SetVersion = "INSERT INTO RealMotionInfo (VersionNumber, Date) VALUES ('" + vb_version.Text + "','" + DateTime.Now.ToString("d/M/yyyy") + "')";
           
            conn = new MySqlConnection(myConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(SetVersion, conn);
         //   lb_version.Text = Convert.ToString(cmd.ExecuteScalar());
            string newversion = Convert.ToString(cmd.ExecuteScalar());
            lb_version.BeginInvoke((Action)delegate() { lb_version.Text = newversion; });
            conn.Close();

            MessageBox.Show("Upload Completed");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string ftplocation = "ftp://ftp.blazargames.com/RealMitionVersions/Updates";
            string file = "/RealMotion" + vb_version.Text.Replace(".", "_") + ".zip";
            string user = "UE4@blazargames.com";
            string password = "File6667788";
            string FileToUpload = file;


            string[] filePaths1 = Directory.GetFiles(ServerFilesLocation, "*", SearchOption.AllDirectories);

            for (int i = 0; i < filePaths1.Length-1;i++)
            {
                FileInfo file2 = new FileInfo(filePaths1[i]);
                string f1 = file2.FullName.Replace(ServerFilesLocation, "");
                FileInfo destFile = new FileInfo(ServerTargetLocation + f1);
                if (destFile.Exists)
                {
                    if (file2.LastWriteTime > destFile.LastWriteTime)
                    {
                        file2.CopyTo(destFile.FullName, true);
                    }
                }
                else
                {
                    string DestFolder = destFile.Directory.ToString();
                    if (!Directory.Exists(DestFolder))
                    {
                        Directory.CreateDirectory(DestFolder);
                    }
                    file2.CopyTo(destFile.FullName, true);
                }
            }

            

            FileStream fs = null;
            Stream rs = null;

            float totalReadBytesCount = 0;

            for (int i = 0; i < FilesWithChanges.Count; i++)
            {
                try
                {
                    string uploadFileName = Path.GetFileName(FilesWithChanges[i]);
                    file = FilesWithChanges[i];
                    string uploadUrl = ftplocation;
                    fs = new FileStream(Destination + file, FileMode.Open, FileAccess.Read);
                    string ftpUrl = string.Format("{0}/{1}", uploadUrl, uploadFileName);
                    FtpWebRequest requestObj = FtpWebRequest.Create(ftpUrl) as FtpWebRequest;
                    requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                    requestObj.Credentials = new NetworkCredential(user, password);
                    rs = requestObj.GetRequestStream();

                    byte[] buffer = new byte[1024];
                    int read = 0;
                    
                    while ((read = fs.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        rs.Write(buffer, 0, read);
                        totalReadBytesCount += buffer.Length;
                        double progress = totalReadBytesCount * 100.0 / TotalSize;
                        if (progress > 100)
                        {
                            progress = 100;
                        }
                        backgroundWorker1.ReportProgress((int)progress);
                    }
                    rs.Flush();
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("File upload/transfer Failed.\r\nError Message:\r\n" + ex.Message, "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }

                    if (rs != null)
                    {
                        rs.Close();
                        rs.Dispose();
                    }
                }
            }
            SetSQL();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Destination = folderBrowserDialog1.SelectedPath;
                tb_MainDir.Text = Destination;
            }
        }

        private void CompareFiles()
        {

         string[] filePaths1 = Directory.GetFiles(tb_MainDir.Text,"*", SearchOption.AllDirectories);

         string[] filePaths2 = Directory.GetFiles(tb_files.Text,"*", SearchOption.AllDirectories);
         FilesWithChanges.Clear();

         for (int i = 0;i < filePaths2.Length; i++)
         {
             bool FoundMatch = false;
             for (int ii = 0; ii < filePaths1.Length; ii++)
             {
                 if (filePaths2[i].Replace(tb_files.Text, "") == filePaths1[ii].Replace(tb_MainDir.Text, ""))
                 {
                     FoundMatch = true;
                     FileInfo File1 = new FileInfo(filePaths1[ii]);
                     FileInfo File2 = new FileInfo(filePaths2[i]);

                     
                     if (File1.Length != File2.Length)
                     {
                         FilesWithChanges.Add(filePaths2[i]);
                     }
                     break;
                 }
             }
             if (FoundMatch == false)
             {
                 FilesWithChanges.Add(filePaths2[i]);
             }
         }
         GetNeededFilesList();
         
        }

        private void GetNeededFilesList()
        {
            string _address = "blazargames.com";
            string _userName = "UE4@blazargames.com";
            string _password = "File6667788";
            string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RealMotion";
            string _localPath = AppData + "temp\\";

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            //System.IO.StreamWriter file = new System.IO.StreamWriter(directory.Parent.FullName + "\\UpdateList.txt");

            string fileName = "RealMitionVersions/Updates/UpdateList.txt";

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + _address + "/" + fileName);
            request.Credentials = new NetworkCredential(_userName, _password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Proxy = null;

            long fileSize; // this is the key for ReportProgress
            using (WebResponse resp = request.GetResponse())
                fileSize = resp.ContentLength;

            request = (FtpWebRequest)WebRequest.Create("ftp://" + _address + "/" + fileName);
            request.Credentials = new NetworkCredential(_userName, _password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            //System.IO.Directory.CreateDirectory(MainDrive + "temp");
            using (FtpWebResponse responseFileDownload = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = responseFileDownload.GetResponseStream())
            using (FileStream writeStream = new FileStream(directory.Parent.FullName + "\\UpdateList.txt", FileMode.Create))
            {

                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                int bytes = 0;

                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                    bytes += bytesRead;// don't forget to increment bytesRead !
                    int totalSize = (int)(fileSize) / 1000; // Kbytes
                    //backgroundWorker2.ReportProgress(100);
                }
            }

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(directory.Parent.FullName + "\\UpdateList.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line == "") continue;
                bool NewFile = false;
                string[] temp = line.Split(':');
                string fileN = temp[0] + temp[1];
                fileN = Path.GetFileName(fileN);
                foreach (string row in FilesWithChanges)
                {
                    string Name = Path.GetFileName(row);
                    if (Name == fileN)
                    {
                        NewFile = true;
                        FileInfo F2 = new FileInfo(row);
                        if (temp[2] == F2.Length.ToString())
                        {
                            NewFile = false;
                            FilesWithChanges.Remove(row);
                            break;
                        }
                    }
                }
                if (NewFile == false)
                {
                    UpdateList.Add(line);
                }
                
            }

            file.Close();


            CreateChangesFile();
        }

        private void CreateChangesFile()
        {
            if (FilesWithChanges.Count > 0)
            { 
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            System.IO.StreamWriter file = new System.IO.StreamWriter(directory.Parent.FullName + "\\UpdateList.txt");

                foreach(string s in UpdateList)
                {
                    file.WriteLine(s);
                }

            foreach (string s in FilesWithChanges)
                {
                    FileInfo f = new FileInfo(s);
                    file.WriteLine(s + ":" + f.Length.ToString());
                }

           
            file.Close();
            FilesWithChanges.Add(directory.Parent.FullName + "\\UpdateList.txt");
            TotalSize = GetTotalSize();
            ReadyToUpload();
            }
            else
            {
                MessageBox.Show("No changes found");
                return;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tb_server.Text = ServerFilesLocation;
        }
    }
}
