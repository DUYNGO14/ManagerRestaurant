var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ManagerRestaurant_Api>("apiservice");

builder.AddProject<Projects.ManagerRestaurant_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
