using OakSoftCore;
using OakSoftCore.MVVM;
using System;
using System.Linq;

namespace OakSoft.ViewModels
{
    public class UserActionRecordViewModel : ViewModelBase
    {
        public Guid UserId { get; set; }

        private string _username;
        public string Username { get => _username; set => TryToSetFieldToNewValue(ref _username, value); }

        private int _userAccountActionTypeId;
        public int UserAccountActionTypeId { get => _userAccountActionTypeId; set => TryToSetFieldToNewValue(ref _userAccountActionTypeId, value); }
        public UserActionRecordType UserAccountActionType { get; set; }

        private DateTime _attemptTimeUtc;
        public DateTime AttemptTimeUtc { get => _attemptTimeUtc; set => TryToSetFieldToNewValue(ref _attemptTimeUtc, value); }

        public DateTime AttemptTimeLocal => AttemptTimeUtc.ToLocalTime();

        private string _actionText;
        public string ActionText { get => _actionText; set => TryToSetFieldToNewValue(ref _actionText, value); }

        public UserActionRecordViewModel() { }

        public void PostMapping(string username)
        {
            Username = username;
            UserAccountActionType = (UserActionRecordType)UserAccountActionTypeId;
            Notify(nameof(Username));
            Notify(nameof(AttemptTimeLocal));
            SetActionText();
        }

        private void SetActionText()
        {
            switch (UserAccountActionType)
            {
                case UserActionRecordType.LoginSuccess:
                    ActionText = $"User Logged in successfully on: {AttemptTimeLocal}.";
                    break;
                case UserActionRecordType.LoginFailure:
                    ActionText = $"User failed to login on: {AttemptTimeLocal}.";
                    break;
                case UserActionRecordType.ChangedSecurityQuestions:
                    ActionText = $"User changed security questions on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.AddedEmail:
                    ActionText = $"User added an email address on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.AddedPhone:
                    ActionText = $"User added a phone number on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.UpdatedEmail:
                    ActionText = $"User updated their email address on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.UpdatedPhone:
                    ActionText = $"User updated their phone number on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.ResetPasswordSuccess:
                    ActionText = $"User successfully reset password by answering security questions on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.ResetPasswordFailure:
                    ActionText = $"User failed to answer security questions successfully on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.TwoFactorAuthenticatedPhone:
                    ActionText = $"User successfully authorized with phone 2Auth on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.TwoFactorAuthenticatedEmail:
                    ActionText = $"User successfully authorized with email 2Auth on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.TwoFactorFailurePhone:
                    ActionText = $"User failed to authorize with phone 2Auth on: {AttemptTimeLocal}";
                    break;
                case UserActionRecordType.TwoFactorFailureEmail:
                    ActionText = $"User failed to authorize with email 2Auth on: {AttemptTimeLocal}";
                    break;
                default:
                    break;
            }
        }
    }
}
