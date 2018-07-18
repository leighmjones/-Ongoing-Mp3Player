using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mp3PlayerLite
{
    class Mp3Player : IDisposable
    {
        // Bool to determine wether or not we will repeat the song.
        public bool Repeat { get; set; }

        public Mp3Player (string fileName)
        {
            //Empty for right now...
        }
       

        [DllImport("winmm.dll")]
        //Allow us to send commands to the API / Pluggable Interface
        private static extern long mciSendString(string strCommand,
           StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        // Play method
        public void Play()
        {
            string command = "play MediaFile";
            if (Repeat) command += " REPEAT";
            // used to send messages to API
            mciSendString(command, null, 0, IntPtr.Zero);
        }// end play method

        // Stop method
        public void Stop()
        {
            string command = "pause MediaFile";
            // used to send messages to API
            mciSendString(command, null, 0, IntPtr.Zero);
        }// end stop method

        //Dispose Method
        public void Dispose()
        {
            string command = "close MediaFile";
            // used to send messages to API
            mciSendString(command, null, 0, IntPtr.Zero);
        }// end dispose method

        //Load Media
        public void LoadMedia(string fileName)
        {
            //Format of the open command that will be sent to API using mciSendString
            const string FORMAT = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = String.Format(FORMAT, fileName);
            // used to send messages to API
            mciSendString(command, null, 0, IntPtr.Zero);
        }// end LoadMedia method


    }
}
