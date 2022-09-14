using OakSoft.Model;
using OakSoftCore;
using OakSoftCore.MVVM;

namespace OakSoft.ViewModels
{
    public class UserSecurityQuestionResponseViewModel : ViewModelBase
    {
        private string _questionText;
        public string QuestionText { get => _questionText; set => TryToSetFieldToNewValue(ref _questionText, value); }

        private int _securityQuestionId;
        public int SecurityQuestionId
        {
            get => _securityQuestionId;
            set
            {
                _securityQuestionId = value;
                SecurityQuestion = (OakSoftCore.SecurityQuestion)value;
            }
        }
        private OakSoftCore.SecurityQuestion _securityQuestion;
        public OakSoftCore.SecurityQuestion SecurityQuestion
        {
            get => _securityQuestion;
            set
            {
                _securityQuestion = value;
                QuestionText = $"{value.ToString().SplitAtCapitals()}?";
                Notify(nameof(QuestionText));
            }
        }
        private string _answer;
        public string Answer
        {
            get => _answer;
            set => TryToSetFieldToNewValue(ref _answer, value);
        }


        private bool _result;
        public bool Result
        {
            get => _result;
            set => TryToSetFieldToNewValue(ref _result, value);
        }

        public bool IsInvalid { get; private set; }

        public UserSecurityQuestionResponseViewModel(UserSecurityQuestion question)
        {
            SecurityQuestionId = question.SecurityQuestionId;
        }
    }
}
