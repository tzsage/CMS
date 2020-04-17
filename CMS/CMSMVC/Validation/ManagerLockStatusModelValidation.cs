using FluentValidation;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSMVC.Validation
{
    public class ManagerLockStatusModelValidation : AbstractValidator<StatusModel>
    {
        public ManagerLockStatusModelValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("主键不能为空");
            RuleFor(x => x.Status).NotNull().WithMessage("状态不能为空");
        }
    }
}
