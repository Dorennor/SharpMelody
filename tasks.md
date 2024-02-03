//TODO auth
//TODO logging
//TODO routing
//TODO endpoints

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
// _use TagSharp for metadata extraction on song add_
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

//------------------------------------------------------------------------------

03.02.2024

VR

0. Finish (refactor) LivraryService (add interface?).
1. Improve initialization and update process of library
2. Rest API naming (api/music/performer/{name} + api/music/performer/?)
3. Endpoints for lists (Skip/take optional params)

OS

1. Finish Docker configuration
2. Endpoints (songs -> id, name)
3. Investigate auth

//TODOLIST

1. Async library initialization
2. Tests
3. Controller => minimal API
