using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FreeFile.DownloadManager;
using FreeFile.DownloadManager.Abstract;
using System.IO;

namespace FreeFiles.UI.WinForm
{
    public partial class FreeFilesForm : Form
    {
        public FreeFilesForm()
        {
            InitializeComponent();
        }
        FileTransferManager fileTransferManager;

        private void FreeFilesForm_Load(object sender, EventArgs e)
        {
            fileTransferManager = new FileTransferManager();
            fileTransferManager.FilePartDownloaded += fileTransferManager_FilePartDownloaded;
        }

        void fileTransferManager_FilePartDownloaded(object sender, DataContainerEventArg<FileTransferManager.FilePartData> e)
        {
            List<Tuple<FreeFile.DownloadManager.FileTransferManager.DownloadParameter, Byte[]>> data = new List<Tuple<FreeFile.DownloadManager.FileTransferManager.DownloadParameter, Byte[]>>();
            data.Add(new Tuple<FreeFile.DownloadManager.FileTransferManager.DownloadParameter, Byte[]>(e.Data.DownloadParameter, e.Data.FileBytes));
            if (e.Data.DownloadParameter.AllPartsCount == e.Data.DownloadParameter.Part)
            {
                saveFile(data);
            }
        }

        private void saveFile(List<Tuple<FileTransferManager.DownloadParameter, byte[]>> data)
        {
            CheckForIllegalCrossThreadCalls = false;
            var lst = data.OrderBy(x => x.Item1.Part).ToList();
            var bytes = new List<byte>();
            for (int i = 0; i <lst.Count; i++)
            {
                bytes.AddRange(lst[i].Item2);
            }
            var dialog= new SaveFileDialog();
            dialog.FileName = Path.GetFileNameWithoutExtension(data[0].Item1.FileSearchResult.FileName);
            dialog.DefaultExt = data[0].Item1.FileSearchResult.FileType;
            dialog.ShowDialog(this);            
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                File.WriteAllBytes(dialog.FileName, bytes.ToArray());
            }
            MessageBox.Show(this, "File saved!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFileName.Text))
            {
                List<Entities.File> foundFileInfoList = fileTransferManager.SearchFileByName(txtFileName.Text);
                this.dataGridView1.DataSource = foundFileInfoList;
            }
        }

     

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Entities.File fileSearchResult = dataGridView1.Rows[e.RowIndex].DataBoundItem as Entities.File;
           this.fileTransferManager.Download(fileSearchResult);
        }

        private void UploadBTN_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //
                // The user selected a folder and pressed the OK button.
                // We print the number of files found.
                //
               List<Entities.File> addedFiles = new List<Entities.File>();
                Entities.Peer peerType = new Entities.Peer();
                
                    Entities.File FileType = new Entities.File();
                    FileInfo fInfo = new FileInfo(openFileDialog1.FileName);
                    FileType.FileName = openFileDialog1.FileName;
                    FileType.FileSize = (int)fInfo.Length;
                    FileType.FileType = Path.GetExtension(openFileDialog1.FileName);
                    FileType.PeerHostName = Config.LocalHostyName;
                    peerType.PeerID=FileType.PeerID = Guid.NewGuid();
                    addedFiles.Add(FileType);
                

                peerType.PeerHostName = Config.LocalHostyName;
                fileTransferManager.AddFiles(addedFiles,peerType);
                MessageBox.Show("Files Added!");
            }
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
