A mapping validation rule from Entity Framework Core.

### Get Started
Walterla.FluentValidation.EntityFrameworkCore can be installed using the Nuget package manager or the `dotnet` CLI.

```
Install-Package Walterla.FluentValidation.EntityFrameworkCore
```

### Dependency Injection
```csharp
using FluentValidation.EFCore;

 public void ConfigureServices(IServiceCollection services)
        {
            // Replace the name of your DbContext.
            services.AddScoped<IDbContextModelDescriptor, DbContextModelDescriptor<YourDbContext>>();
        }
```

### Example
```csharp
using FluentValidation.EFCore;

public class CreateCustomerCommandValidator: DbAbstractValidator<CreateCustomerCommand> {
  public CreateCustomerCommandValidator(
    IDbContextModelDescriptor dbContextModelDescriptor) : base(dbContextModelDescriptor) 
  {
            CreateRuleMap<Customer>(dest => dest.Postcode, opt => opt.CustomerPostcode)
                .NotEmptyFromEntity()
                // Mapping to DbContext's Fluent API(.HasMaxLength(5)) or Data Annotations([MaxLength(5)])
                .MaximumLengthFromEntity()
                .ToRuleBuilderOptions()
                .Must(BeAValidPostcode)
                .WithMessage("Please specify a valid postcode");
  }

  private bool BeAValidPostcode(string postcode) {
    // custom postcode validating logic goes here
  }
}

var createCustomerCommand = new CreateCustomerCommand();
var validator = new CreateCustomerCommandValidator();
ValidationResult results = validator.Validate(createCustomerCommand);

bool success = results.IsValid;
IList<ValidationFailure> failures = results.Errors;
```


### License, Copyright etc

Walterla.FluentValidation.EntityFrameworkCore is copyright &copy; 2020 Walter-la and other contributors. It's free for anyone.