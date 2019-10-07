using System;
using System.Windows;
using System.Windows.Threading;

namespace OutrunClock {
    public partial class MainWindow : Window {

        private readonly string[] MonthWords;

        public MainWindow() {
            InitializeComponent();

            MonthWords = new string[] {
                "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"
            };

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs eventArgs) {
            this.Hour.Text = NumberToWords(((DateTime.Now.TimeOfDay.Hours + 11) % 12) + 1);
            this.Date.Text = $"{MonthWords[DateTime.Now.Month - 1]} {DateTime.Now.Day}";
            var minutes = "";

            if (DateTime.Now.Minute < 10)
                minutes = "ZERO ";

            this.Minutes.Text = $"{minutes}{NumberToWords(DateTime.Now.Minute)}";
        }

        private string NumberToWords(int number) {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0) {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0) {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0) {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0) {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20) {
                    words += unitsMap[number];
                } else {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words.ToUpper();
        }
    }
}