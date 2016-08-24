using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TinyClient.Api;

namespace TinyClient.Command
{
    public class RelayCommand<T> : ICommand
    {

        #region Declarations

        readonly Predicate<T> _canExecute;
        readonly Action<T> _execute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RelayCommand class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(Object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }
    public class RelayCommand : ICommand
    {
        #region Declarations

        readonly Func<Boolean> _canExecute;
        readonly Action _execute;

        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<Boolean> canExecute)
        {

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion
        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(Object parameter)
        {
            _execute();
        }

        #endregion
    }

    public class ShowProfile : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Common.TinyMainWindow.MainFrame.Source = new Uri("Pages/PageProfile.xaml#user_id=" + ((Types.profile)parameter).id, UriKind.Relative);
        }
    }

    public class PlayPause : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
            //return Common.MusicPlayer.Playlist.Count != 0;
        }

        public void Execute(object parameter)
        {
            Common.MusicPlayer.PlayPause();
        }
    }

    public class PlayNext : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
            //return Common.MusicPlayer.Playlist.Count != 0;
        }

        public void Execute(object parameter)
        {
            Common.MusicPlayer.PlayNext();
        }
    }

    public class PlayPrev : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
            //return Common.MusicPlayer.Playlist.Count != 0;
        }

        public void Execute(object parameter)
        {
            Common.MusicPlayer.PlayPrev();
        }
    }

}
