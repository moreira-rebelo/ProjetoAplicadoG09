using ISI.Application.Interfaces;
using ISI.Application.UseCases.Reservation.CreateEntry;
using ISI.Application.UseCases.Reservation.CreateReservation;
using ISI.Application.UseCases.Reservation.GetReservation;
using ISI.Application.UseCases.Reservation.LoginReservation;
using ISI.Application.UseCases.User.GetUser;
using ISI.Domain.Repository;
using ISI.infrastructure;
using ISI.infrastructure.Repositories;
using MediatR;


namespace ISI.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReservation).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetReservation).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUser).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEntry).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginReservation).Assembly));
        services.AddRepositories();
        return services;
    }
    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IReservationRepository, ReservationRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}