using Business.Handlers.GeneralDatas.Commands;
using FluentValidation;

namespace Business.Handlers.GeneralDatas.ValidationRules
{
    public class CreateGeneralDataValidator : AbstractValidator<CreateGeneralDataCommand>
    {
        public CreateGeneralDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.PlayersDifficultylevel).NotNull();
        }
    }

    public class UpdateGeneralDataValidator : AbstractValidator<UpdateGeneralDataCommand>
    {
        public UpdateGeneralDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.PlayersDifficultylevel).NotNull();
        }
    }
}