using Metro.Kids.Models;
using Metro.Kids.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Windows.UI.Xaml;

namespace Metro.Kids.ViewModels
{
    public class MathPageViewModel : ViewModelBase
    {
        public MathConfig Config { get; set; }
        private MathOperationGenerator _mathGenerator = new MathOperationGenerator();
        private StorageServices _storeSevices = new StorageServices();
        private ObservableCollection<SessionRecord> _sessionRecords;

        public ObservableCollection<SessionRecord> SessionRecords { get { return _sessionRecords; } set { Set(() => SessionRecords, ref _sessionRecords, value); }}
        public MathPageViewModel()
        {
            //Config = new MathConfig();
            InitializeData();
            InitializeCommand();
            InitializeOther();
        }

        private void InitializeOther()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);

        }

        private void Timer_Tick(object sender, object e)
        {
            _AccumultedTime += timer.Interval;
            ElapsedTime = _AccumultedTime.ToString("hh\\:mm\\:ss");
        }


        private void InitializeCommand()
        {
            PlayOrPauseStudyCommand = new DelegateCommand(HandlePlayOrPauseCommand);
        }

        private void InitializeData()
        {
            MinNumber = 2;
            MaxNumber = 9;
            CountPerCal = 25;
            SelectedNumberCount = 3;
            IsPlusChecked = true;
            IsMultChecked = true;
            IsDivChecked = false;
            IsMinusChecked = false;
            IsDivEnabled = true;
            IsPlusEnabled = true;
            IsMinusEnabled = true;
            IsMultEnabled = true;
            NumberCountList = new ObservableCollection<int>();
            SessionRecords = _storeSevices.GetAllSessionRecords();
            for (int i = 2; i < 5; i++)
            {
                NumberCountList.Add(i);
            }            
            SingleHistories = new ObservableCollection<SignleRecord>();
        }

        internal void EndSession()
        {
            IsSession = false;
            var sessionRec = new SessionRecord();
            if (SingleHistories.Count > 10 || SingleHistories.Count >= CountPerCal)
            {
                sessionRec.StartTime = SingleHistories.First().StartTime;
                sessionRec.Duration = new TimeSpan(SingleHistories.Sum(x => x.Duration.Ticks));
                sessionRec.CollectionCount = SingleHistories.Count();
                sessionRec.ErrorCount = SingleHistories.Sum(x => x.ErrorCount);
                sessionRec.CorrectRate = string.Format("{0:00} %", sessionRec.CollectionCount * 100 / (sessionRec.CollectionCount + sessionRec.ErrorCount));
                SingleHistories.Clear();
                AddSessionRecord(sessionRec);
            }
            if(IsStudying == true)
            PlayOrPauseStudyCommand.Execute(null);
        }

        public void StartNewSession()
        {
            IsSession = true;
            if(IsStudying == false)
            PlayOrPauseStudyCommand.Execute(null);
            //clear the data grid
            SingleHistories.Clear();

            //reset the index;
            QuestionIndex = 0;
            ShowNextMathOperation();
        }

        private void AddSessionRecord(SessionRecord sessionRec)
        {
            SessionRecords.Insert(0, sessionRec);
            _storeSevices.Insert(sessionRec);
        }

        public void ShowNextMathOperation()
        {
            InputAnswer = null;
            QuestionIndex++;
            MathOperation = GenerteMathOperation();
            _currentSingleRecord = new SignleRecord
            {
                RecordId = QuestionIndex,
                StartTime = DateTime.Now,
                ErrorCount = 0,
            };
        }

        private string GenerteMathOperation()
        {
            var factors = new List<NumberFactors>();
            if(IsMultChecked)
            factors.Add(NumberFactors.MULITPLE);
            if(IsPlusChecked)
            factors.Add(NumberFactors.PLUS);
            if(IsMinusChecked)
            {
                factors.Add(NumberFactors.MINUS);
            }
            if(IsDivChecked)
            {
                factors.Add(NumberFactors.DIV);
            }
            return _mathGenerator.GenerateMathOperate(new MathSeed
            {
                MinNumber = MinNumber,
                MaxNumber = MaxNumber,
                CountNumber = SelectedNumberCount,
                Factors = factors,
            });

        }

        private int _MinNumber;
        private int _MaxNumber;
        private int _CountPerCal;
        private string _ElapsedTime;

        private int _SelectedNumberCount;

        private bool _IsStudying;
        private string _MathOperation;
        private bool _IsShowingResult;
        private bool _IsCorrectResult;
        private double? _InputAnswer;
        private int _QuestionIndex;

        private bool _IsSession;


        private bool _IsPlusChecked;
        private bool _IsPlusEnabled;

        public bool IsPlusChecked { get { return _IsPlusChecked; }  set { Set(() => IsPlusChecked, ref _IsPlusChecked, value); } }
        public bool IsPlusEnabled { get { return _IsPlusEnabled; } set { Set(() => IsPlusEnabled, ref _IsPlusEnabled, value); } }


        private bool _IsMinusChecked;
        private bool _IsMinusEnabled;
        public bool IsMinusChecked { get { return _IsMinusChecked; } set { Set(() => IsMinusChecked, ref _IsMinusChecked, value); } }
        public bool IsMinusEnabled { get { return _IsMinusEnabled; } set { Set(() => IsMinusEnabled, ref _IsMinusEnabled, value); } }


        private bool _IsMultChecked;
        private bool _IsMultEnabled;
        public bool IsMultChecked { get { return _IsMultChecked; } set { Set(() => IsMultChecked, ref _IsMultChecked, value); } }
        public bool IsMultEnabled { get { return _IsMultEnabled; } set { Set(() => IsMultEnabled, ref _IsMultEnabled, value); } }


        private bool _IsDivChecked;
        private bool _IsDivEnabled;
        public bool IsDivChecked { get { return _IsDivChecked; } set { Set(() => IsDivChecked, ref _IsDivChecked, value); } }
        public bool IsDivEnabled { get { return _IsDivEnabled; } set { Set(() => IsDivEnabled, ref _IsDivEnabled, value); } }

        private bool _IsShowingDraftBoard;
        public bool IsShowingDraftBoard { get { return _IsShowingDraftBoard; } set { Set(() => IsShowingDraftBoard, ref _IsShowingDraftBoard, value); } }


        public bool IsSession
        {
            get { return _IsSession; }
            set { Set(()=>IsSession, ref _IsSession, value); }
        }

        private ObservableCollection<SignleRecord> _SingleHistories;

        public ObservableCollection<SignleRecord> SingleHistories
        {
            get
            {
                return _SingleHistories;
            }
            set
            {
                Set(() => SingleHistories, ref _SingleHistories, value);
            }
        }

        public string MathOperation
        {
            get
            {
                return _MathOperation;
            }
            set
            {
                Set(() => MathOperation, ref _MathOperation, value);
            }
        }

        public bool IsStudying
        {
            get
            {
                return _IsStudying;
            }
            set
            {
                Set(() => IsStudying, ref _IsStudying, value);
            }
        }
       
        public int MinNumber { get { return _MinNumber; } set { Set(() => MinNumber, ref _MinNumber, value); } }
        public int MaxNumber { get { return _MaxNumber; } set { Set(() => MaxNumber, ref _MaxNumber, value); } }
        public int CountPerCal { get { return _CountPerCal; } set { Set(() => CountPerCal, ref _CountPerCal, value); } }
        public bool IsShowingResult { get { return _IsShowingResult; } set { Set(() => IsShowingResult, ref _IsShowingResult, value); } }
        public bool IsCorrectResult { get { return _IsCorrectResult; } set { Set(() => IsCorrectResult, ref _IsCorrectResult, value); } }

        public string ElapsedTime { get { return _ElapsedTime; } set { Set(() => ElapsedTime, ref _ElapsedTime, value); } }

        public double? InputAnswer { get { return _InputAnswer; } set { Set(() => InputAnswer, ref _InputAnswer, value); } }
        public int SelectedNumberCount { get { return _SelectedNumberCount; } set { Set(() => SelectedNumberCount, ref _SelectedNumberCount, value); } }

        public int QuestionIndex { get { return _QuestionIndex; } set { Set(() => QuestionIndex, ref _QuestionIndex, value); } }
        public ObservableCollection<int> NumberCountList { get; set; }


        public ICommand PlayOrPauseStudyCommand;
        private DispatcherTimer timer;
        private TimeSpan _AccumultedTime;
        private bool _IsNewPractice = true  ;
        private SignleRecord _currentSingleRecord;

        public void HandlePlayOrPauseCommand()
        {
            IsStudying = !IsStudying;
            if(IsStudying)
            {
                if (_IsNewPractice)
                {
                    _AccumultedTime = TimeSpan.Zero;
                    _IsNewPractice = false;
                }
                
                timer.Start();                
            }
            else
            {
                timer.Stop();
            }
        }

        public void ValidateAnswer()
        {            
            try
            {
                IsCorrectResult = Math.Round(InputAnswer.Value, 2) == Math.Round(MathEvaluator.EvaluteByNCalc(MathOperation), 2);
            }
            catch(Exception e)
            {
                IsCorrectResult = false;
            }
            
            if(IsCorrectResult)
            {             
                _currentSingleRecord.Duration = _AccumultedTime;
                SingleHistories.Add(_currentSingleRecord);
                if(QuestionIndex >= CountPerCal)
                {
                    EndSession();
                }
                else
                {
                    ShowNextMathOperation();
                }
                
            }
            else
            {
                _currentSingleRecord.ErrorCount++;
                IsShowingResult = true;
            }      
        }
    }
}
