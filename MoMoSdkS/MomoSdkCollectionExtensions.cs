﻿using Microsoft.Extensions.DependencyInjection;
using MoMoSdk.Services;
using MoMoSdk.Utils;

namespace MomoSdk;

public static class MomoSdkCollectionExtensions
{
    public static void AddMomoSdk(this IServiceCollection services)
    {
        services.AddScoped<IMomoHttpClient,MomoHttpClient>();
        services.AddScoped<IMomoService,MomoService>();
    }
}