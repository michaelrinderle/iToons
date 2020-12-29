using iToons.Library.Entity;
using iToons.Mobile.Data;
using iToons.Mobile.Models;
using MediaManager;
using MediaManager.Library;
using MediaManager.Playback;
using MediaManager.Queue;
using System;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace iToons.Mobile.ViewModels
{
    public class MainPageViewModel : BasePageViewModel
    {
        public ICommand PlayPauseCommand
        {
            get
            {
                return new Command(() => { PlayPauseAudio(); });
            }
        }

        public ICommand NextSongCommand
        {
            get
            {
                return new Command(() =>
                {
                    NextSongAudio();
                });
            }
        }

        public ICommand ShuffleCommand
        {
            get
            {
                return new Command(() =>
                {
                    ShuffleAudio();
                });
            }
        }

        private Api API { get; set; }

        private int SongID { get; set;} = 0;
        private string SongUrl { get; set; }

        private IMediaItem MediaItem { get; set;}

        private MetaData _Current { get; set; }
        public MetaData Current {
            get => _Current;
            set
            {
                if(_Current == value) return;
                _Current = value;
                OnPropertyChanged(nameof(Current));
            }
        }

        private Color _ShuffleButton = Color.LightGray;
        public Color ShuffleButton
        {
            get => _ShuffleButton;
            set
            {
                if(_ShuffleButton == value) return;
                _ShuffleButton = value;
                OnPropertyChanged(nameof(ShuffleButton));
            }
        }

        private string _PlayPauseText = "Pause";
        public string PlayPauseText
        {
            get => _PlayPauseText;
            set
            {
                if (_PlayPauseText == value) return;
                _PlayPauseText = value;
                OnPropertyChanged(nameof(PlayPauseText));
            }
        }

        private double _AudioPointer { get; set;}
        public double AudioPointer
        {
            get => _AudioPointer;
            set
            {
                _AudioPointer = value;
                // CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(value));
                OnPropertyChanged(nameof(AudioPointer));
            }
        }

        private double _SongDuration { get; set;} = 100;
        public double SongDuration
        {
            get => _SongDuration;
            set
            {
                if (_SongDuration == value) return;
                _SongDuration = value;
                OnPropertyChanged(nameof(SongDuration));
            }
        }

        public MainPageViewModel()
        {
            LoadViewAndSettings();
            StartAudio();
        }

        private  void LoadViewAndSettings()
        {
            API = new Api();
            CrossMediaManager.Current.AutoPlay = true;
            CrossMediaManager.Current.ShuffleMode = ShuffleMode.Off;
            CrossMediaManager.Current.MediaItemFinished += Current_MediaItemFinished;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
        }

        private async void StartAudio(int songID = 0)
        {
             await CrossMediaManager.Current.Stop();
            Current = await API.GetMetaData(songID);
            SongUrl = Constants.GetBaseStreamUrl(Current.Id);
            SongID = Current.Id;
            MediaItem =  await CrossMediaManager.Current.Play(SongUrl);
            SongDuration = (int)MediaItem.Duration.TotalSeconds;
            AudioPointer = 0;
            MediaItem.MetadataUpdated += (sender, args) => {
                Console.Write(args.MediaItem.Title);
            };
        }

        private void PlayPauseAudio()
        {
            if (CrossMediaManager.Current.IsPlaying())
                PlayPauseText = "Play";
            else
                PlayPauseText = "Pause";
            CrossMediaManager.Current.PlayPause();
        }

        private void NextSongAudio()
        {
            Thread.Sleep(TimeSpan.FromSeconds(.5));
            if(CrossMediaManager.Current.ShuffleMode != ShuffleMode.All)
            {
                SongID++;
                StartAudio(SongID);
            }
            else
                StartAudio();
        }

        private void ShuffleAudio()
        {
            if(CrossMediaManager.Current.ShuffleMode == ShuffleMode.All)
            {
                ShuffleButton = Color.LightGray;
                CrossMediaManager.Current.ShuffleMode = ShuffleMode.Off;
            }
            else
            {
                ShuffleButton = Color.Green;
                CrossMediaManager.Current.ShuffleMode = ShuffleMode.All;
            }
        }

        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            AudioPointer = (int)e.Position.TotalSeconds;
        }

        private void Current_MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            NextSongAudio();
        }
    }
}
