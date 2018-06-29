using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lesson2_ControlCorral
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<ReservationInfo> _reservations;
        GenericCommand _command;
        List<string> _usuals = new List<string>();

        public MainPage()
        {
            this.InitializeComponent();
            _reservations = new List<ReservationInfo>();
            Loaded += MainPage_Loaded;
            _command = new GenericCommand();
            _command.DoSomthing += _command_DoSomthing;
            control_name.TextChanged += Control_name_TextChanged;
            control_name.SuggestionChosen += Control_name_SuggestionChosen;
            control_name.QuerySubmitted += Control_name_QuerySubmitted;
        }

        private void Control_name_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var searchTerm = args.QueryText.ToLower();
            var result = _usuals.Where(i => i.Contains(searchTerm)).ToList();
            control_name.ItemsSource = result;
            control_name.IsSuggestionListOpen = true;
        }

        private void Control_name_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            control_name.Text = args.SelectedItem as string;
        }

        private void Control_name_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent())
            {
                var searchTerm = control_name.Text.ToLower();
                var result = _usuals.Where(i => i.Contains(searchTerm)).ToList();
                control_name.ItemsSource = result;
            }
        }

        async private void _command_DoSomthing(string command)
        {
            if (command.ToLower() == "make a reservation")
            {
                if (control_calendar.Date != null)
                {
                    var newReservation = new ReservationInfo
                    {
                        AppointmentDay = control_calendar.Date.Value.Date,
                        AppointmentTime = control_time.Time,
                        CustomerName = control_name.Text
                    };
                    _reservations.Add(newReservation);
                    var md = new MessageDialog($"{_reservations.Count} messages reserved\n"
                        + $"Newest is on {newReservation.AppointmentDay.Month}/"
                        + $" {newReservation.AppointmentDay.Day}/"
                        + $" {newReservation.AppointmentDay.Year}"
                        + $" at {newReservation.AppointmentTime}\n"
                        + $"for {newReservation.CustomerName}");
                    await md.ShowAsync();
                    control_calendar.Date = null;
                }
                else
                {
                    var md = new MessageDialog("Select a date first");
                    await md.ShowAsync();
                }
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _command;
            _usuals = new List<string> { "alex", "azuka",
                "elizabeth",
                "ahmed","josh","allan", "john","david","chris","jack",
                "bobby","ike","emeka","tobe","chidi","mason","andrew" };
            control_name.ItemsSource = _usuals;
        }

        private void ReserveMessage(object sender, RoutedEventArgs e)
        {
            var newReservation = new ReservationInfo();
            _reservations.Add(newReservation);
        }
    }
}
