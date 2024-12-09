var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var aspireDB = sqlServer.AddDatabase("aspireDB");

var seq = builder.AddSeq("seq");

var razor = builder.AddProject<Projects.AspirePoc_RazorApp>("razorService")
    .WithReference(seq)
    .WithReference(aspireDB)
    .WaitFor(aspireDB);

builder.Build().Run();
