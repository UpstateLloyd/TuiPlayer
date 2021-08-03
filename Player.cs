using System.Threading;
using NAudio.Wave;
using Terminal.Gui;

namespace TuiPlayer
{    
    public class Player
    {
        private string lastFileOpened;
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        
        public void PlayAudioFile() //Method implements audio playback from a file  
        {
            string file = @"C:\Users\lhotchkiss\Music\A Favor House Atlantic.mp3";

            using var audioFile = new AudioFileReader(file);    // Load the audio file
            using var outputDevice = new WaveOutEvent();        // Select an output device  

            outputDevice.Init(audioFile);
            outputDevice.Play();

            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }        

        private void OpenFile()
        {
            var d = new OpenDialog("Open", "Open an audio file") { AllowsMultipleSelection = false}; // Create a new open dialog
            Application.Run(d);

            if (!d.Canceled)
            {
                lastFileOpened = d.FilePath.ToString();

                Play(d.FilePath.ToString()); // Pass the chosen path to the player
            }
        }

        private void Play(string path)
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += onPlaybackStopped;
            }

            if (audioFile == null)
            {
                try
                {
                    audioFile = new AudioFileReader(path);
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                }
            }

            if (audioFile != null)
            {
                try
                {
                    outputDevice.Play();
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                }
            }
        }

        private void Pause()
        {
            try
            {
                outputDevice?.Pause();
            }
            catch (System.NullReferenceException)
            {
            }
        }

        //Dispose of device once playback is stopped
        //This will be changed later
        private void onPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
        }
    }    
}
