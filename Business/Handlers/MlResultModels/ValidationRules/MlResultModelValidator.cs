
using Business.Handlers.MlResultModels.Commands;
using FluentValidation;

namespace Business.Handlers.MlResultModels.ValidationRules
{

    public class CreateMlResultModelValidator : AbstractValidator<CreateMlResultModelCommand>
    {
        public CreateMlResultModelValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ResultValue).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();

        }
    }
    public class UpdateMlResultModelValidator : AbstractValidator<UpdateMlResultModelCommand>
    {
        public UpdateMlResultModelValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ResultValue).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();

        }
    }
}