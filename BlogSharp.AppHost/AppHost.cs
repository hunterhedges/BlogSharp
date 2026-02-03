var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BlogSharp>("blogsharp");

builder.Build().Run();
