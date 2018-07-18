using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Mp3PlayerLite
{
    public partial class Form1 : Form
    {
        
        //class objects
        private Mp3Player _mp3Player;
        private File files;

        //List to hold & store the files that are uploaded
        List<string> songs = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        public void LoadPlayList()
        {
            songs.Clear();
            listView1.Items.Clear();

            using (OpenFileDialog myFolder = new OpenFileDialog())
            {
                myFolder.Filter = "Playlist File|*.plst";
                foreach (string file in myFolder.FileNames)
                {
                    string fileInfo = Path.GetFileNameWithoutExtension(file);

                    listView1.Items.Add(file);
                    songs.Add(file);
                }
            } 

        }

        #region PLAY BUTTON
        private void btnPlay_Click(object sender, EventArgs e)
        {
            //Checks to see if there is a file in the listbox before playing
            if (_mp3Player != null)
            {
              

                if (listView1.SelectedIndices.Count > 0)
                {
                    //Cancel the current song from playing
                    _mp3Player.Dispose();

                    //Load the song that is selected
                    for (int x = 0; x < listView1.SelectedItems.Count; x++)
                    {
                        _mp3Player.LoadMedia(songs[listView1.SelectedIndices[x]]);
                    }

                    //Play
                    _mp3Player.Play();
                }
            }
        }
        #endregion
        
        #region STOP/PAUSE BUTTON
        private void btnStop_Click(object sender, EventArgs e)
        {
            //Stop/Pause the music
            if (_mp3Player != null)
                _mp3Player.Dispose();
        }
        #endregion

        #region OPEN FILE
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog myFolder = new OpenFileDialog())
            {
                //Lets you select multiple files at once
                myFolder.Multiselect = true;

                myFolder.Filter = "Mp3 File|*.mp3; | Wave File|*.wav| PlayList File|*.plst";
                myFolder.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                if (myFolder.ShowDialog() == DialogResult.OK)
                {
                    //Shows dialog box
                    _mp3Player = new Mp3Player(myFolder.FileName);
                    //Repeat mode
                    //_mp3Player.Repeat = false;
                }

                foreach (string file in myFolder.FileNames)
                {
                    //Displays file name without .mp3 & .wav extension
                    string fileInfo = Path.GetFileNameWithoutExtension(file);
                 
                    listView1.Items.Add(fileInfo);
                    songs.Add(file);



                    //Enable buttons once listView is populated
                    btnPlay.Enabled = true;
                    button1.Enabled = true;
                    btnStop.Enabled = true;
                    prevButton.Enabled = true;
                    nextButton.Enabled = true;
                }

               

            }
        }
        #endregion

        #region PREVIOUS BUTTON
        private void prevButton_Click(object sender, EventArgs e)
        {
            int maxNum = listView1.Items.Count;
            
            if (listView1.SelectedIndices.Count > 0)
            {
                int oldSelection = listView1.SelectedIndices[0];
                listView1.SelectedIndices.Clear();

                if (oldSelection - 1 >= listView1.Items.Count)
                {
                    listView1.SelectedIndices.Add(0);

                }//end if

                else
                {
                    listView1.SelectedIndices.Add(oldSelection - 1);
                    listView1.Focus();

                    for (int x = 0; x < listView1.SelectedItems.Count; x++)
                    {
                        _mp3Player.Dispose();
                        _mp3Player.LoadMedia(songs[listView1.SelectedIndices[x]]);
                    }
                    
                    //Play
                    _mp3Player.Play();

                }//end else
            }

           /* if (listView1.SelectedIndices.Count == -1)
            {
               

            }*/
        }
        #endregion

        #region NEXT BUTTON
        private void nextButton_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedIndices.Count > 0)
            {
                int oldSelection = listView1.SelectedIndices[0];
                listView1.SelectedIndices.Clear();

                if (oldSelection + 1 >= listView1.Items.Count)
                {
                    listView1.SelectedIndices.Add(0);

                }//end if

                else
                {
                    listView1.SelectedIndices.Add(oldSelection + 1);
                    listView1.Focus();

                    for (int x = 0; x < listView1.SelectedItems.Count; x++)
                    {
                        _mp3Player.Dispose();
                        _mp3Player.LoadMedia(songs[listView1.SelectedIndices[x]]);
                    }

                    //Play
                    _mp3Player.Play();

                }//end else

            }
        }
        #endregion

        //Double Click Event
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                //Cancel the current song from playing
                _mp3Player.Dispose();

                //Load the song that is selected
                for (int x = 0; x < listView1.SelectedItems.Count; x++)
                {
                    _mp3Player.LoadMedia(songs[listView1.SelectedIndices[x]]);
                }
                
                //Play
                _mp3Player.Play();
            }
        }

        //Repeat Picture/Button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Checks to see if the listView is empty, if there are files it will execute the following code...
            if (listView1.SelectedItems.Count > 0)
            {
               // _mp3Player.Repeat = true;
                onePic.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mp3Player.Stop();
           
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFile.FileName);
                saveFile.Filter = "PlayList File | *.plst";
                writer.WriteLine(songs);
                writer.Close();
            }
        }

        private void openPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadPlayList();
        }


        #region YELLOW SKIN
        private void button2_Click(object sender, EventArgs e)
        {
            btnPlay.BackColor = Color.Khaki;
            button1.BackColor = Color.Khaki;
            btnStop.BackColor = Color.Khaki;
            nextButton.BackColor = Color.Khaki;
            prevButton.BackColor = Color.Khaki;
            this.BackColor = Color.Gold;
            listView1.BackColor = Color.LemonChiffon;
            listView1.ForeColor = Color.Black;
        
        }
        #endregion 

        #region BLUE SKIN
        private void button3_Click(object sender, EventArgs e)
        {
            btnPlay.BackColor = Color.LightSkyBlue;
            button1.BackColor = Color.LightSkyBlue;
            btnStop.BackColor = Color.LightSkyBlue;
            nextButton.BackColor = Color.LightSkyBlue;
            prevButton.BackColor = Color.LightSkyBlue;
            this.BackColor = Color.DodgerBlue;
            listView1.BackColor = Color.LightBlue;
            listView1.ForeColor = Color.Black;
        }
        #endregion

        #region PURPLE SKIN
        private void button4_Click(object sender, EventArgs e)
        {
            btnPlay.BackColor = Color.MediumPurple;
            button1.BackColor = Color.MediumPurple;
            btnStop.BackColor = Color.MediumPurple;
            nextButton.BackColor = Color.MediumPurple;
            prevButton.BackColor = Color.MediumPurple;
            this.BackColor = Color.Indigo;
            listView1.BackColor = Color.BlueViolet;
            listView1.ForeColor = Color.White;
        }
        #endregion

        #region BLACK SKIN
        private void button5_Click(object sender, EventArgs e)
        {
            btnPlay.BackColor = Color.Black;
            button1.BackColor = Color.Black;
            btnStop.BackColor = Color.Black;
            nextButton.BackColor = Color.Black;
            prevButton.BackColor = Color.Black;
            this.BackColor = Color.Black;
            listView1.BackColor = Color.DimGray;
            listView1.ForeColor = Color.White;

        }
        #endregion 

        #region PINK SKIN
        private void button6_Click(object sender, EventArgs e)
        {

            btnPlay.BackColor = Color.Salmon;
            button1.BackColor = Color.Salmon;
            btnStop.BackColor = Color.Salmon;
            nextButton.BackColor = Color.Salmon;
            prevButton.BackColor = Color.Salmon;
            this.BackColor = Color.LightSalmon;
            listView1.BackColor = Color.MistyRose;
            listView1.ForeColor = Color.White;
        }
        #endregion
    }
}
