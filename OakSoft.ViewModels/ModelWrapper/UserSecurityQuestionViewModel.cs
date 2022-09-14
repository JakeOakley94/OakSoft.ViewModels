using OakSoftCore;
using OakSoftCore.MVVM;
using System;

namespace OakSoft.ViewModels
{
    public class UserSecurityQuestionViewModel : ViewModelBase
    {
        public Guid UserId { get; set; }
        public int SecurityQuestionId { get; set; }
        public SecurityQuestion SecurityQuestion { get; set; }

        public void PostMapping()
        {
            SecurityQuestion = (SecurityQuestion)SecurityQuestionId;
        }

    }


}
