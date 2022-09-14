using OakSoft.Model;
using OakSoftCore.Mapping;
using OakSoftCore.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OakSoft.ViewModels
{
    public class TimeIntervalViewModel : WrapperViewModelBase<TimeInterval>
    {
        private const int daysInWeek = 7;
        public ObservableCollection<int> AllDays { get; set; }
        public ObservableCollection<int> AllWeeks { get; set; }
        public ObservableCollection<int> AllMonths { get; set; }
        public ObservableCollection<int> AllYears { get; set; }

        private string _name;
        public string Name { get => _name; set => TryToSetFieldToNewValue(ref _name, value); }

        private int _days;
        [Range(0, 6)]
        public int Days { get => _days; set => TryToSetFieldToNewValue(ref _days, value); }

        private int _weeks;
        [Range(0, 51)]
        public int Weeks { get => _weeks; set => TryToSetFieldToNewValue(ref _weeks, value); }

        private int _months;
        [Range(0, 11)]
        public int Months { get => _months; set => TryToSetFieldToNewValue(ref _months, value); }

        private int _years;
        [Range(0, 10)]
        public int Years { get => _years; set => TryToSetFieldToNewValue(ref _years, value); }


        private DateTime _endOfTimeIntervalFromToday;
        public DateTime EndOfTimeIntervalFromToday { get => _endOfTimeIntervalFromToday; set => TryToSetFieldToNewValue(ref _endOfTimeIntervalFromToday, value); }

        public TimeIntervalViewModel() { }

        // new wrapper.
        public TimeIntervalViewModel(string name, int minutes, int hours, int days, int weeks, int months, int years)
        {
            isInitialized = true;
            Id = Guid.NewGuid();
            Name = name;
            Days = days;
            Weeks = weeks;
            Months = months;
            Years = years;
            PostInitialization();
        }

        public void PostInitialization()
        {
            InitObjects();
            PopulateTimeRanges();
        }
        private void InitObjects()
        {
            AllDays = new ObservableCollection<int>();
            AllWeeks = new ObservableCollection<int>();
            AllMonths = new ObservableCollection<int>();
            AllYears = new ObservableCollection<int>();
        }

        private void PopulateTimeRanges()
        {
            AllDays = PopulateRangeOfTime(0, 6);
            AllWeeks = PopulateRangeOfTime(0, 3);
            AllMonths = PopulateRangeOfTime(0, 11);
            AllYears = PopulateRangeOfTime(0, 10);
        }

        private ObservableCollection<int> PopulateRangeOfTime(int start, int end)
        {
            ObservableCollection<int> toSet = new ObservableCollection<int>();

            for (int i = start; i <= end; i++)
            {
                toSet.Add(i);
            }
            return toSet;
        }


        internal void GetExpirationFromToday() => EndOfTimeIntervalFromToday = AddTimePeriods(DateTime.Now);
        internal DateTime GetTimeIntervalExpirationFromAnotherDate(DateTime lastTime) => AddTimePeriods(lastTime);

        internal bool HasExpired(DateTime expirationDate)
        {
            var timeNow = DateTime.Now;
            expirationDate = AddTimePeriods(expirationDate);
            return (timeNow > expirationDate);
        }

        private DateTime AddTimePeriods(DateTime time)
        {
            time = time.AddDays(Days);
            time = time.AddDays(daysInWeek * Weeks);
            time = time.AddMonths(Months);
            time = time.AddYears(Years);
            return time;
        }

        public override TimeInterval BuildModel(OakSoftMapper mapper) => mapper.MapTo<TimeInterval>(this);
    }
}
