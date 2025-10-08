using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MotoRental.Application.DTOs;
using MotoRental.Application.Validators;
using MotoRental.Application.Handlers;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<MotorcycleCreateDtoValidator>();

        services.AddScoped<RegisterMotorcycleCommandHandler>();
        services.AddScoped<GetMotorcyclesQueryHandler>();
        services.AddScoped<UpdateMotorcyclePlateCommandHandler>();
        services.AddScoped<DeleteMotorcycleCommandHandler>();
        services.AddScoped<RegisterRiderCommandHandler>();
        services.AddScoped<UploadCnhImageCommandHandler>();
        services.AddScoped<CreateRentalCommandHandler>();
        services.AddScoped<ReturnRentalCommandHandler>();

        return services;
    }
}
