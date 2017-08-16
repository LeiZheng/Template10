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
            MinNumber = 1;
            MaxNumber = 9;
            CountPerCal = 25;
            SelectedNumberCount = 2;
            NumberCountList = new ObservableCollection<int>();
            for (int i = 2; i < 3; i++)
            {
                NumberCountList.Add(i);
            }            
            SingleHistories = new ObservableCollection<SignleRecord>();
        }

        public void ShowNextMathOperation()
        {
            InputAnswer = null;
            QuestionIndex++;
            MathOperation = GenerteMathOperation();
            QuestionIndex = 1;
            _currentSingleRecord = new SignleRecord
            {
                RecordId = QuestionIndex,
                StartTime = DateTime.Now,
                ErrorCount = 0,
            };

            MathOperation = GenerteMathOperation();

        }

        private string GenerteMathOperation()
        {
            var factors = new List<NumberFactors>();
            factors.Add(NumberFactors.MULITPLE);
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

        private ObservableCollection<SignleRecord> SingleHistories { get; set; }

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
            IsCorrectResult = InputAnswer == MathEvaluator.Evaluate(MathOperation);
            if(IsCorrectResult)
            {
                ShowNextMathOperation();
            }
            else
            {
                _currentSingleRecord.ErrorCount++;
                IsShowingResult = true;
            }      
        }
    }
}
