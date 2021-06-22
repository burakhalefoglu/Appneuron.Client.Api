
using Business.Handlers.Arpus.Commands;
using FluentValidation;

namespace Business.Handlers.Arpus.ValidationRules
{

    public class CreateArpuValidator : AbstractValidator<CreateArpuCommand>
    {
        public CreateArpuValidator()
        {

        }
    }
    public class UpdateArpuValidator : AbstractValidator<UpdateArpuCommand>
    {
        public UpdateArpuValidator()
        {

        }
    }
}