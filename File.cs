using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Mp3PlayerLite 
{
    class File 
    {
        //Backing fields
        private string _currentFile;
        private string _nextFile;
        private string _prevFile;
        private bool _shuffleMode { get; set; } = false;

        //List object
        private List<File> Playlst = new List<File>();

        //File Constructor
        public File()
        {
            _currentFile = "";
            _nextFile = "";
            _prevFile = "";
            _shuffleMode = false;
        }

        //Current File Property
        public string CurrentFile
        {
            get { return _currentFile; }
            set { _currentFile = value; }
        }

        //Previous File Property
        public string PrevFile
        {
            get { return _prevFile; }
            set { _prevFile = value; }
        }

        //Next File Property
        public string NextFile
        {
            get { return _nextFile; }
            set { _nextFile = value; }
        }

        //?????
        public void LoadPlayList()
        {

            
        }

        //?????
        public void LoadFolder()
        {
            
        }
        
    }
}
