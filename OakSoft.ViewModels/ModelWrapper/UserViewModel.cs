using AutoMapper;
using OakSoft.Model;
using OakSoftCore.Mapping;
using OakSoftCore.MVVM;
using System.Collections.ObjectModel;

namespace OakSoft.ViewModels
{
    public class UserViewModel : WrapperViewModelBase<User>
    {
        private string _username;
        public string Username { get => _username; set => TryToSetFieldToNewValue(ref _username, value); }

        private string _firstName;
        public string FirstName { get => _firstName; set => TryToSetFieldToNewValue(ref _firstName, value); }

        private string lastName;
        public string LastName { get => lastName; set => TryToSetFieldToNewValue(ref lastName, value); }

        private string _email;
        public string Email { get => _email; set => TryToSetFieldToNewValue(ref _email, value); }

        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => TryToSetFieldToNewValue(ref _phoneNumber, value); }

        private DateTime _lastPasswordResetDate;
        public DateTime LastPasswordResetDate { get => _lastPasswordResetDate; set => TryToSetFieldToNewValue(ref _lastPasswordResetDate, value); }

        public bool IsLockedOut { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }

        #region Not Implemented / Future
        private bool _useTwoFactorAuthentication;
        public bool UseTwoFactorAuthentication { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        #endregion

        public ObservableCollection<UserActionRecordViewModel> LoginAttempts { get; set; } = new ObservableCollection<UserActionRecordViewModel>();
        public ObservableCollection<UserSecurityQuestionViewModel> UserSecurityQuestions { get; set; } = new ObservableCollection<UserSecurityQuestionViewModel>();

        public UserViewModel()
        {

        }
        public override User BuildModel(OakSoftMapper mapper) => mapper.MapTo<User>(this);

    }
}
