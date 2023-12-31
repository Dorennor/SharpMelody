var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//TODO auth
app.UseHttpsRedirection();

//TODO logging

//TODO routing

//TODO endpoints

app.Run();

// TODO VR
//========================================================
// files storage
// - model band/singer { Name, Albums, MetaGenre, ID }
// - album model { Songs, Singer, Genre(s), ID }
// - song model { Title, Genre, Metadata, Band, Album, ID }
// - metadata model { Dictionary<string, string> }

// create folder for music
// structure -> root => singer => albums => songs

// Library Refresh
// 1. on server startup
// 2. refresh endpoint
// OR file watcher

// (I)MusicRepository
// CR-U
// *use TagSharp for metadata extraction on song add*
// unit tests for edge cases + happy path
// testing framework - xUnit, nUint
// red - green - refactor
// AAA pattern

// endpoints for songs listing
// - for singer
// - for albums
// - for song

// TODO OS
//=================================
// EF Core -> add 
// table for metadata for persistence
// Research DB (PostgreSQL) for metadata storage
// Songs metadata => artist | title | album | year | genre | length | filename
// Genre/singer statistics