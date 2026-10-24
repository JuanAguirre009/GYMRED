using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GymRed.Infrastructure.Data.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(GymRedDbContext context)
    {
        await SeedSedesAsync(context);
        await SeedInstructoresAsync(context);
        await SeedPlanesAsync(context);
        await SeedClientesAsync(context);
        await SeedClasesAsync(context);
        await context.SaveChangesAsync();
    }

    private static async Task SeedSedesAsync(GymRedDbContext context)
    {
        if (await context.Sedes.AnyAsync())
            return;

        Sede sedePrincipal = new(
            "Sede Principal",
            "Calle 123 # 45-67",
            "6011234567",
            "principal@gymred.com",
            new DateTime(2020, 1, 15));

        await context.Sedes.AddAsync(sedePrincipal);
    }

    private static async Task SeedInstructoresAsync(GymRedDbContext context)
    {
        if (await context.Instructores.AnyAsync())
            return;

        Instructor carlos = new(
            "Carlos",
            "Gomez",
            "carlos.gomez@gymred.com",
            "3001112233",
            "Entrenamiento funcional");

        Instructor ana = new(
            "Ana",
            "Rodriguez",
            "ana.rodriguez@gymred.com",
            "3004445566",
            "Yoga y pilates");

        await context.Instructores.AddRangeAsync(carlos, ana);
    }

    private static async Task SeedPlanesAsync(GymRedDbContext context)
    {
        if (await context.Planes.AnyAsync())
            return;

        Plan mensual = new(
            "Mensual",
            "Acceso ilimitado por 30 dias",
            30,
            new Dinero(120_000),
            "Acceso a todas las sedes, clases grupales y zona de pesas");

        Plan trimestral = new(
            "Trimestral",
            "Acceso ilimitado por 90 dias",
            90,
            new Dinero(300_000),
            "Acceso a todas las sedes, clases grupales, zona de pesas y 1 sesion de nutricion");

        await context.Planes.AddRangeAsync(mensual, trimestral);
    }

    private static async Task SeedClientesAsync(GymRedDbContext context)
    {
        if (await context.Clientes.AnyAsync())
            return;

        Sede? sede = await context.Sedes.FirstOrDefaultAsync();
        if (sede is null)
            return;

        Cliente cliente = new(
            "Laura",
            "Martinez",
            "1000123456",
            "laura.martinez@correo.com",
            "3109876543",
            new DateTime(1995, 5, 20),
            sede.Id);

        await context.Clientes.AddAsync(cliente);
    }

    private static async Task SeedClasesAsync(GymRedDbContext context)
    {
        if (await context.Clases.AnyAsync())
            return;

        Instructor? instructor = await context.Instructores.FirstOrDefaultAsync();
        Sede? sede = await context.Sedes.FirstOrDefaultAsync();

        if (instructor is null || sede is null)
            return;

        Clase crossfit = new(
            "CrossFit",
            "Entrenamiento de alta intensidad",
            instructor.Id,
            sede.Id,
            20);

        await context.Clases.AddAsync(crossfit);
    }
}
