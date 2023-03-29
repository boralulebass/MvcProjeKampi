using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adını boş geçemezsiniz.");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar soyadını boş geçemezsiniz.");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmını boş geçemezsiniz.");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Unvan kısmını boş geçemezsiniz.");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail kısmını boş geçemezsiniz.");
            RuleFor(x => x.WriterName).MinimumLength(3).WithMessage("Lütfen en az 2 karakter giriniz.");
            RuleFor(x => x.WriterName).MaximumLength(30).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayınız.");

        }
    }
}
