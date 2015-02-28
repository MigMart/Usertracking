using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;


namespace Usertracking
{
    public class SpeechRecorder
    {
        public KinectSensor kinectsensor;
        private const EncodingFormat AudioFormat = EncodingFormat.Pcm;
        private const int AudioSamplesPerSecond = 16000;
        private const int AudioBitsPerSample = 16;
        private const int AudioChannels = 1;
        private const int AudioAverageBytesPerSecond = 32000;
        private const int AudioBlockAlign = 2;
        private const int AngleRetentionPeriod = 1000;
        private const double DefaultConfidenceThreshold = 0.3;
        /// <summary>
        /// Engine used to configure and control speech recognition behavior.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;
        /// <summary>
        /// Kinect audio source used to stream audio data from Kinect sensor.
        /// </summary>
        private KinectAudioSource kinectAudioSource;
        
        private Grammar grammar;

        public SpeechRecorder(KinectSensor sensor)
        {
            kinectsensor = sensor;

            RecognizerInfo recognizerInfo = GetKinectRecognizer();

            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.Grammar)))
            {
                grammar = new Grammar(memoryStream, "MouseCommands");
            }

            this.speechEngine = new SpeechRecognitionEngine(recognizerInfo);
            speechEngine.LoadGrammar(grammar);
        }

        ~SpeechRecorder()
        {
            this.speechEngine.Dispose();
            this.speechEngine = null;
        }

        public bool Start()
        {
            if (kinectsensor.Status == KinectStatus.Connected)
            {
                return StartRecording(kinectsensor.AudioSource);
            }
            else
            {
                return false;
            }
        }

        private bool StartRecording(KinectAudioSource audioSource)
        {
            if (null == audioSource)
            {
                return false;
            }

            this.kinectAudioSource = audioSource;
            this.kinectAudioSource.AutomaticGainControlEnabled = false;
            this.kinectAudioSource.NoiseSuppression = true;
            this.kinectAudioSource.BeamAngleMode = BeamAngleMode.Adaptive;

            //this.kinectAudioSource.SoundSourceAngleChanged += this.SoundSourceChanged;
            this.speechEngine.SpeechRecognized += this.SpeechRecognizedFiltered;
            //this.speechEngine.SpeechRecognitionRejected += this.SreSpeechRecognitionRejected;

            var kinectStream = this.kinectAudioSource.Start();
            this.speechEngine.SetInputToAudioStream(
                kinectStream, new SpeechAudioFormatInfo(AudioFormat, AudioSamplesPerSecond, AudioBitsPerSample, AudioChannels, AudioAverageBytesPerSecond, AudioBlockAlign, null));
            this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);

            return true;
        }

        /// <summary>
        /// Stop streaming Kinect audio data and recognizing speech.
        /// </summary>
        public void Stop()
        {
            if (this.kinectAudioSource != null)
            {
                this.kinectAudioSource.Stop();
                //this.speechEngine.RecognizeAsyncCancel();
                //this.speechEngine.RecognizeAsyncStop();

                //this.kinectAudioSource.SoundSourceAngleChanged -= this.SoundSourceChanged;
                //this.speechEngine.SpeechRecognized -= this.SpeechRecognizedFiltered;
                //this.speechEngine.SpeechRecognitionRejected -= this.SreSpeechRecognitionRejected;
            }
        }

        /// <summary>
        /// Gets the metadata for the speech recognizer (acoustic model) most suitable to
        /// process audio from Kinect device.
        /// </summary>
        /// <returns>
        /// RecognizerInfo if found, <code>null</code> otherwise.
        /// </returns>
        private static RecognizerInfo GetKinectRecognizer()
        {
            Func<RecognizerInfo, bool> matchingFunc = r =>
            {
                string value;
                r.AdditionalInfo.TryGetValue("Kinect", out value);
                return "True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(r.Culture.Name, StringComparison.OrdinalIgnoreCase);
            };
            return SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();
        }

        /// <summary>
        /// Handler for rejected speech events.
        /// </summary>
        /// <param name="sender">
        /// Object sending the event.
        /// </param>
        /// <param name="e">
        /// Event arguments.
        /// </param>
        private void SpeechRecognizedFiltered(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence < DefaultConfidenceThreshold)
            {
                return;
            }

            SpeechRecorded(this, new RecordedEventArgs { Phrase = e.Result.Text, SemanticValue = e.Result.Semantics.Value.ToString() });
        }

        public event EventHandler<RecordedEventArgs> SpeechRecorded;
    }

    

    /// <summary>
    /// Event arguments for RecordedEventArgs.
    /// </summary>
    public class RecordedEventArgs : EventArgs
    {
        /// <summary>
        /// Speech phrase (text) recognized.
        /// </summary>
        public string Phrase { get; set; }

        /// <summary>
        /// Semantic value associated with recognized speech phrase.
        /// </summary>
        public string SemanticValue { get; set; }
    }
}
