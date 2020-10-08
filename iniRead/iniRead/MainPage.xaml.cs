using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Security.Permissions;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace iniRead
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".ini");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                FileNameTextBox.Text = file.Path;
                var content = File.ReadAllText(file.Path);
            }
            else
            {
                FileNameTextBox.Text = "Operation cancelled.";
            }
        }
        private async void TextBlock_Updated(object sender, RoutedEventArgs e)
        {
            updateFile();
        }
       

        public async void updateFile()
        {
            //Create an instance of a ini file parser
            var parser = new FileIniDataParser();
            var content = File.ReadAllText(FileNameTextBox.Text);
            IniData parsedData = parser.ReadData(File.OpenText(FileNameTextBox.Text));

            if(parsedData.Sections.Count == 0)
            {

            }
            else
            {
                parsedData["COMMON"]["TSPort2 "] = "100";
            }

            parser.WriteFile(FileNameTextBox.Text, parsedData);
        } 
    }

}
