using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;

namespace PolarisEcl.Application.Common.Validators;

public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
{
    public RefreshTokenRequestDtoValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required");
    }
}


public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator(IAppDbContext appDbContext)
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .WithMessage("First name is required.");

        RuleFor(x => x.LastName)
        .NotEmpty()
        .WithMessage("Last name is required.");

        RuleFor(x => x.Email)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Email is required")
        .EmailAddress()
        .WithMessage("Invalid email address format.")
        .MustAsync(async (email, cancellationToken) =>
        {
            return !await appDbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
        })
        .WithMessage("This email is already registered");

        RuleFor(x => x.Password)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Password is required")
        .MinimumLength(6)
        .WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Role)
        .NotEmpty()
        .IsInEnum()
        .WithMessage("A valid user role is required.");

        RuleFor(x => x.IsActive).Equal(true).WithMessage("New user registration must be set to Active");
    }
}

public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserRequestDtoValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("Request body payload cannot be null.");
        When(x => x.FirstName != null, () =>
        {
            RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("First name cannot be empty when provided.");
        });

        When(x => x.LastName != null, () =>
       {
           RuleFor(x => x.LastName)
                   .NotEmpty().WithMessage("Last name cannot be empty when provided.");
       });

        When(x => x.Email != null, () =>
        {
            RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email cannot be empty when provided.")
                .EmailAddress().WithMessage("Invalid email address format.");
        });


        When(x => x.Role != null, () =>
        {
            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("The provided user role is invalid.");
        });
    }
}

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Email)
        .Cascade(CascadeMode.Stop)
      .NotEmpty().WithMessage("Email is required")
      .EmailAddress()
      .WithMessage("Invalid email address format.");

        RuleFor(x => x.Password)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Password is required")
        .MinimumLength(6)
        .WithMessage("Password must be at least 6 characters long.");
    }
}

public class BulkDeleteRequestDtoValidator : AbstractValidator<BulkDeleteRequestDto>
{
    public BulkDeleteRequestDtoValidator()
    {
        RuleFor(x => x.userIds)
        .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("No user IDs provided.")
            .Must(ids => ids != null && ids.Count > 0).WithMessage("At least one user ID must be specified.");
    }
}