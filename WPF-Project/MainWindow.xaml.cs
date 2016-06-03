using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Dictionary<int, Year> yearsDict;
        static List<EventDay> eventList;
        static List<Note> noteList;

        static Year actualYearSelected;
        static Month actualMonthSelected;
        static Day actualDaySelected;

        static double loadingOpacity = 0.4;
        static int alertTimer = 0;
        static bool isAlertThreadRunning = false;

        public MainWindow()
        {
            int myThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Started main thread. ID : " + myThreadId);

            InitializeComponent();
            yearsDict = new Dictionary<int, Year>();
            dat.SelectionMode = DataGridSelectionMode.Single;

            eventList = new List<EventDay>();
            noteList = new List<Note>();

            DateTime todays = DateTime.Now;
            Month month = generateMonth(todays.Year, todays.Month);

            actualDaySelected = month.getDay(todays.Day);
            actualMonthSelected = month;
            actualYearSelected = new Year(todays.Year);

            yearsDict.Add(todays.Year, actualYearSelected);
            yearsDict[actualYearSelected.YearID].monthsDict.Add(todays.Month, month);

            MonthBlock.Text = month.MonthNames[month.MonthID] + " " + month.MonthYear;
            dat.ItemsSource = month.Weeks;
            refreshTodayEvents();

            //ThreadPool.QueueUserWorkItem(new WaitCallback(AlertThread));
        }

        /// <summary>
        /// Import danych JSON, stworzenie nowego kalendarza i wyświetlenie obecnego miesiąca
        /// </summary>
        /// <param name="data"></param>
        public void loadImportedData(List<DayJson> data)
        {
            yearsDict = new Dictionary<int, Year>();
            eventList = new List<EventDay>();
            noteList = new List<Note>();
            dat.ItemsSource = null;

            foreach (var date in data.Select(d => d.date))
            {
                if (!yearsDict.ContainsKey(date.Year))
                {
                    Year yearTemp = new Year(date.Year);
                    Month monthTemp = generateMonth(date.Year, date.Month);
                    yearTemp.monthsDict.Add(date.Month, monthTemp);
                    yearsDict.Add(date.Year, yearTemp);
                }
                else
                {
                    if (!yearsDict[date.Year].monthsDict.ContainsKey(date.Month))
                    {
                        Month monthTemp = generateMonth(date.Year, date.Month);
                        yearsDict[date.Year].monthsDict.Add(date.Month, monthTemp);
                    }
                }
            }

            foreach (var datas in data)
            {
                if (datas.eventsList.Count > 0)
                    yearsDict[datas.date.Year].monthsDict[datas.date.Month].getDay(datas.date.Day).hasEvents = true;

                if (eventList.Count <= 0)
                {
                    eventList = datas.eventsList;
                }
                else
                    eventList = eventList.Concat(datas.eventsList).ToList();
            }

            foreach (var datas in data)
            {
                if (datas.noteList.Count > 0)
                    yearsDict[datas.date.Year].monthsDict[datas.date.Month].getDay(datas.date.Day).hasNotes = true;

                if (noteList.Count <= 0)
                {
                    noteList = datas.noteList;
                }
                else
                    noteList = noteList.Concat(datas.noteList).ToList();
            }

            DateTime todays = DateTime.Now;
            if (yearsDict.ContainsKey(todays.Year))
            {
                if (yearsDict[todays.Year].monthsDict.ContainsKey(todays.Month))
                {
                    actualDaySelected = yearsDict[todays.Year].monthsDict[todays.Month].getDay(todays.Day);
                    actualMonthSelected = yearsDict[todays.Year].monthsDict[todays.Month];
                    actualYearSelected = yearsDict[todays.Year];
                }
                else
                {
                    Month month = generateMonth(todays.Year, todays.Month);
                    actualDaySelected = month.getDay(todays.Day);
                    actualMonthSelected = month;
                    yearsDict[actualYearSelected.YearID].monthsDict.Add(todays.Month, month);

                }
            }
            else
            {
                Month month = generateMonth(todays.Year, todays.Month);
                actualDaySelected = month.getDay(todays.Day);
                actualMonthSelected = month;
                actualYearSelected = new Year(todays.Year);

                yearsDict.Add(todays.Year, actualYearSelected);
                yearsDict[actualYearSelected.YearID].monthsDict.Add(todays.Month, month);
            }

            MonthBlock.Text = actualMonthSelected.MonthNames[actualMonthSelected.MonthID] + " " + actualMonthSelected.MonthYear;
            dat.ItemsSource = actualMonthSelected.Weeks;
            refreshTodayEvents();
        }

        public void refreshTodayEvents()
        {
            DateTime today = DateTime.Now;
            List<EventDay> events = (from n in eventList
                                     where n.date.Year == today.Year && n.date.Month == today.Month && n.date.Day == today.Day
                                     select n).ToList();
            TodayEvents.ItemsSource = events.OrderBy(e => e.date);
            if (events.Count <= 0)
            {
                ShowTodayEventsButton.IsChecked = false;
                ShowTodayEventsButton.IsEnabled = false;
                TodayEvents.Visibility = Visibility.Collapsed; 
            }
            else
                ShowTodayEventsButton.IsEnabled = true;
        }

        /// <summary>
        /// Wygenerowanie miesiąca dla określonego roku oraz numeru miesiąca
        /// </summary>
        /// <param name="yearId"></param>
        /// <param name="monthId"></param>
        /// <returns></returns>
        private Month generateMonth(int yearId, int monthId)
        {
            Month month = new Month();
            List<Week> weeks = new List<Week>();
            Week w = new Week();
            int DaysTotal = DateTime.DaysInMonth(yearId, monthId);
            int DaysCount = 1;
            for (int i = 0; i <= 7; i++)
            {
                if (DaysCount <= DaysTotal)
                {

                    DateTime dt = new DateTime(yearId, monthId, DaysCount);
                    int dayOfWeek = (dt.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)dt.DayOfWeek - 1;
                    w.day[dayOfWeek] = new Day();
                    w.day[dayOfWeek].date = dt;

                    if (dt.DayOfWeek == DayOfWeek.Sunday || DaysCount == DaysTotal)
                    {
                        weeks.Add(w);
                        w = new Week();
                        i = 0;
                    }
                    DaysCount++;
                }
            }

            month.MonthID = monthId;
            month.MonthYear = yearId;
            DateTime date = new DateTime(yearId, monthId, 1);
            month.Name = date.ToString("MMM", CultureInfo.InvariantCulture);
            month.Weeks = weeks;
            return month;
        }

        /// <summary>
        /// Zwraca notatkę o danej dacie
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private Note getNoteByDate(DateTime date)
        {
            foreach (var note in noteList)
            {
                if (note.date == date)
                    return note;
            }
            return new Note();
        }

        /// <summary>
        /// Wyświetlenie poprzedniego miesiąca
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousMonthButton_Click(object sender, RoutedEventArgs e)
        {
            Year searchedYear;
            Month searchMonth;
            if ((actualMonthSelected.MonthID - 1) < 1)
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID - 1))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID - 1;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID - 1];
            }
            else
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID];
            }


            if ((actualMonthSelected.MonthID - 1) < 1)
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(12))
                {
                    searchMonth = generateMonth(searchedYear.YearID, 12);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[12];
                }
            }
            else
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(actualMonthSelected.MonthID - 1))
                {
                    searchMonth = generateMonth(searchedYear.YearID, actualMonthSelected.MonthID - 1);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[actualMonthSelected.MonthID - 1];
                }
            }

            dat.ItemsSource = searchMonth.Weeks;
            actualMonthSelected = searchMonth;
            actualYearSelected = searchedYear;
            MonthBlock.Text = actualMonthSelected.MonthNames[actualMonthSelected.MonthID] + " " + actualYearSelected.YearID;
        }

        /// <summary>
        /// Wyświetlenie następnego miesiąca
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMonthButton_Click(object sender, RoutedEventArgs e)
        {
            Year searchedYear;
            Month searchMonth;
            if ((actualMonthSelected.MonthID + 1) > 12)
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID + 1))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID + 1;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID + 1];
            }
            else
            {
                if (!yearsDict.ContainsKey(actualYearSelected.YearID))
                {
                    searchedYear = new Year();
                    searchedYear.YearID = actualYearSelected.YearID;
                    yearsDict.Add(searchedYear.YearID, searchedYear);
                }
                else
                    searchedYear = yearsDict[actualYearSelected.YearID];
            }


            if ((actualMonthSelected.MonthID + 1) > 12)
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(1))
                {
                    searchMonth = generateMonth(searchedYear.YearID, 1);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[1];
                }
            }
            else
            {
                if (!yearsDict[searchedYear.YearID].monthsDict.ContainsKey(actualMonthSelected.MonthID + 1))
                {
                    searchMonth = generateMonth(searchedYear.YearID, actualMonthSelected.MonthID + 1);
                    yearsDict[searchedYear.YearID].monthsDict.Add(searchMonth.MonthID, searchMonth);
                }
                else
                {
                    searchMonth = searchedYear.monthsDict[actualMonthSelected.MonthID + 1];
                }
            }

            dat.ItemsSource = searchMonth.Weeks;
            actualMonthSelected = searchMonth;
            actualYearSelected = searchedYear;
            MonthBlock.Text = actualMonthSelected.MonthNames[actualMonthSelected.MonthID] + " " + actualYearSelected.YearID;

        }

        /// <summary>
        /// Dodanie wydarzenia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var menuItem = (MenuItem)sender;
                var contextMenu = (ContextMenu)menuItem.Parent;
                var item = (DataGrid)contextMenu.PlacementTarget;
                var index = item.CurrentCell.Column.DisplayIndex;

                var week = (Week)item.SelectedCells[0].Item;
                var day = week.day[index];
                if (day != null)
                {
                    AddEventDlg dlg = new AddEventDlg();
                    dlg.DateText = day.date.ToShortDateString();
                    if (dlg.ShowDialog() == true)
                    {
                        eventList.Add(new EventDay(day.date, dlg.Time, dlg.TitleText, dlg.DescriptionText));
                        day.hasEvents = true;
                        dat.Items.Refresh();
                    }
                }
            }
            catch { }
            refreshTodayEvents();
        }

        /// <summary>
        /// Dodanie notatki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var menuItem = (MenuItem)sender;
                var contextMenu = (ContextMenu)menuItem.Parent;
                var item = (DataGrid)contextMenu.PlacementTarget;
                var index = item.CurrentCell.Column.DisplayIndex;

                var week = (Week)item.SelectedCells[0].Item;
                var day = week.day[index];
                if (day != null)
                {
                    AddNoteDlg dlg = new AddNoteDlg();
                    if (dlg.ShowDialog() == true)
                    {
                        noteList.Add(new Note(day.date, dlg.NoteContent));
                        day.hasNotes = true;
                        dat.Items.Refresh();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Okno dialogowe notatek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNoteDlg dlg = new ShowNoteDlg(noteList, yearsDict);
            this.Opacity = loadingOpacity;
            if (dlg.ShowDialog() == false)
            {
                noteList = dlg.noteList;
            }
            dat.Items.Refresh();
            this.Opacity = 1;
        }

        /// <summary>
        /// Okno dialogowe wydarzeń
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowEventsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEventsDlg dlg = new ShowEventsDlg(eventList, yearsDict);
            this.Opacity = loadingOpacity;
            if (dlg.ShowDialog() == false)
            {
                eventList = dlg.eventsList;
            }
            dat.Items.Refresh();
            refreshTodayEvents();
            this.Opacity = 1;
        }

        /// <summary>
        /// Eksport do pliku danych typu JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportDataButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Archiwum Kalendarza (*.json)|*.json";
            this.Opacity = loadingOpacity;
            if (dlg.ShowDialog() == true)
            {
                using (StreamWriter file = File.CreateText(dlg.FileName))
                {
                    List<DateTime> dateList = new List<DateTime>();
                    foreach (var date in noteList.Select(n => n.date).Distinct())
                    {
                        dateList.Add(date);
                    }

                    foreach (var date in eventList.Select(ev => ev.date).Distinct())
                    {
                        dateList.Add(date);
                    }
                    dateList = dateList.Distinct().ToList();
                    List<DayJson> daysList = new List<DayJson>();

                    foreach (var date in dateList)
                    {
                        DayJson day = new DayJson(date);
                        List<Note> noteTemp = new List<Note>();
                        List<EventDay> eventTemp = new List<EventDay>();
                        foreach (var note in noteList.Where(n => n.date == date))
                        {
                            noteTemp.Add(note);
                        }
                        day.noteList = noteTemp;
                        foreach (var events in eventList.Where(ev => ev.date == date))
                        {
                            eventTemp.Add(events);
                        }
                        day.eventsList = eventTemp;
                        daysList.Add(day);
                    }

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, daysList);
                }
            }
            this.Opacity = 1;

        }

        /// <summary>
        /// Import z pliku danych typu JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportDataButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Archiwum Kalendarza (*.json)|*.json";
            this.Opacity = loadingOpacity;
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    List<DayJson> daysJson = JsonConvert.DeserializeObject<List<DayJson>>(File.ReadAllText(dlg.FileName));
                    if (daysJson != null && daysJson.Count > 0)
                        loadImportedData(daysJson);
                    else
                        MessageBox.Show("ERROR : File is empty!", "WPF-Calendar");
                }
                catch
                {
                    MessageBox.Show("ERROR : Wrong data!", "WPF-Calendar");
                }
            }
            this.Opacity = 1;
        }

        /// <summary>
        /// Pobiera aktualny obrazek z BingPicture
        /// </summary>
        /// <returns><c>True</c> jeśli uda się pobrać obrazek,<c>False</c> w przypadku błędu lub braku aktualnego obrazka.</returns>
        private async Task<bool> setBackgroundAsync()
        {
            try
            {
                Bing BingImgInfo;
                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync("http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US");
                    BingImgInfo = JsonConvert.DeserializeObject<Bing>(json);
                }

                var image = new Image();
                var fullFilePath = @"http://www.bing.com" + BingImgInfo.images[0].url;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;
                this.Background = new ImageBrush(image.Source);
                this.Background.Opacity = 0.70;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Zmienia tło aplikacji oraz style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChangeStyle_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeStyle.IsChecked == true)
            {
                if (await setBackgroundAsync() == true)
                    Application.Current.Resources["DayTextForeground"] = Application.Current.Resources["White"];
            }
            else
            {
                Application.Current.Resources["DayTextForeground"] = Application.Current.Resources["Black"];
                this.Background = Application.Current.Resources["White"] as SolidColorBrush;
            }
        }


        private void ShowTodayEventsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowTodayEventsButton.IsChecked == true)
                TodayEvents.Visibility = Visibility.Visible;
            else
                TodayEvents.Visibility = Visibility.Collapsed;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsDlg dlg = new SettingsDlg(alertTimer, isAlertThreadRunning);
            this.Opacity = loadingOpacity;
            if (dlg.ShowDialog() == true)
            {
                alertTimer = dlg.settingAlert;
                isAlertThreadRunning = dlg.alertToRun;
                if (isAlertThreadRunning)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(AlertThread));
                }
            }
            this.Opacity = 1;
        }

        

        private void AlertThread(object state)
        {
            int myThreadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Started alert thread. ID : " + myThreadId);
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            while (isAlertThreadRunning)
            {
                try
                {
                    if (alertTimer != 0)
                    {
                        DateTime today = DateTime.Now;
                        var events = from e in eventList
                                     where e.date.Year == today.Year && e.date.Month == today.Month && e.date.Day == today.Day
                                     select e;

                        foreach (var eventt in events)
                        {
                            if (!eventt.alertMade)
                            {
                                if ((eventt.date.TimeOfDay - today.TimeOfDay).TotalMinutes <= alertTimer)
                                {
                                    eventt.alertMade = true;
                                    MessageBox.Show("Przypomnienie: " + eventt.name + " w ciągu " + alertTimer + " minut!", "WPF-Calendar");
                                }
                            }

                        }

                    }
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {

                }
            }
            resetEvent.Set();
            //Thread.CurrentThread.Abort();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ///Dopilnować zamknięcia wątku alertu
        }
    }



}