using System.Windows.Input;
using SimpleViewModel2.Commands;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace SimpleViewModel2.ViewModels
{
    class App : Base
    {
        private readonly string originalWindowName = "WPF Notepad";

	    #region Property Changing Variables

	        // Window's name
	        private string _windowName;
            public string windowName {    get { return _windowName; } set { _windowName = value; RaisePropertyChanged(); }  }

           #region Commands
             // Choose file
             private ICommand _openFileCmd;
             public ICommand openFileCmd {   get { return _openFileCmd; } set { _openFileCmd = value; RaisePropertyChanged(); }  }

             // Save selected file
             private ICommand _saveFileCmd;
             public ICommand saveFileCmd {   get { return _saveFileCmd; } set { _saveFileCmd = value; RaisePropertyChanged(); }  }
	       #endregion

	        // Selected file
	        private string _selectedFile;
            public string selectedFile {    get { return _selectedFile; } set { _selectedFile = value; RaisePropertyChanged(); }    }

            // Text from last read file
            private string _fileText;
            public string fileText {    get { return _fileText; } set { _fileText = value; RaisePropertyChanged(); }   }

	    #endregion

		public App()
        {
            windowName = originalWindowName;
            fileText = "";
            openFileCmd = new RelayCommand(ChooseFile);
            saveFileCmd = new RelayCommand(SaveFile);
        }

        /// <summary>
        /// Adds additional text to the existing application's title
        /// </summary>
        private void ChangeWindowName(string name = "")
        {
            windowName = $"{originalWindowName} - {name}";
        }

        /// <summary>
        /// Reads and displays the text from a text file
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void LoadFile(string file)
        {
            try
            {
                fileText = File.ReadAllText(file);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        /// <summary>
        /// Overrides the loaded file's content with 
        /// the provided string
        /// </summary>
        private void SaveFile(object _text)
        {
            try
            {
                string text = (string)_text;

                var shouldSave = new SaveFileDialog();
                shouldSave.Title = "Save file";
                shouldSave.FileName = selectedFile;
                shouldSave.DefaultExt = ".txt";
                shouldSave.Filter = "Text Documents (.txt) | *.txt";

                if (shouldSave.ShowDialog() == true)
                {
                    string saveTo = shouldSave.FileName;
                    File.WriteAllText(saveTo, text);
                    ChangeWindowName(Path.GetFileName(saveTo));
                }
            }
            catch (Exception e) // Could be due to an invalid cast or IO error when saving..
            {
                MessageBox.Show(e.Message, "Failed to save file!", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
		}

        /// <summary>
        /// Lets the user select a text file on their system
        /// </summary>
        private void ChooseFile()
        {
			var filePick = new OpenFileDialog();
            filePick.Filter = "Text documents (.txt) | *.txt";

            if (filePick.ShowDialog() == true)
			{
                selectedFile = filePick.FileName;

                try
                {
                    LoadFile(selectedFile);
                    ChangeWindowName(Path.GetFileName(selectedFile)); // <App name> - something.txt
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Failed to load file!", MessageBoxButton.OK, MessageBoxImage.Error);
                }     
            }
		}
    }
}
