using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() 
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı alanını boş geçemezsiniz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konuyu boş geçemezsiniz.");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("En az 3 karakter girişi yapın.");
            RuleFor(x => x.MessageContent).MinimumLength(3).WithMessage("En az 3 karakter girişi yapın.");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayın.");
        }
    }
}
