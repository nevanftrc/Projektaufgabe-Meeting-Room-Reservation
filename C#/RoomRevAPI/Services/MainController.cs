using MongoDB.Bson;
using MongoDB.Driver;
using RoomRevAPI.Models;
using System.Collections.Generic;

namespace RoomRevAPI.Services
{
    /// <summary>
    /// This class manages the monog db creation and others i guess
    /// </summary>
    public class MainController
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;

        // ✅ MongoDB Collections
        public IMongoCollection<MeetingRooms> MeetingRooms => _database.GetCollection<MeetingRooms>("MeetingRooms");
        public IMongoCollection<Reservers> Reservers => _database.GetCollection<Reservers>("Reservers");
        public IMongoCollection<Reservations> Reservations => _database.GetCollection<Reservations>("Reservations");

        // ✅ Constructor Fix
        public MainController(MongoClient client, IMongoDatabase database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
            //InitializeDatabase();   used for connection test
            SchemaCreator();
            //TestInserter();
            //DMlSettings(configuration); commented out for reasons
        }

        private static readonly Guid StaticRoomId = Guid.NewGuid(); // ✅ Only generated once

        private void TestInserter()
        {
            var toolsList = new List<Tools>
            {
                new Tools { Name = "Projector", Description = "Desc", Count = 1 },
                new Tools { Name = "Beamer", Description = "tesct", Count = 2 }
            };

            var room = new MeetingRooms
            {
                RoomRevNr = StaticRoomId,  // ✅ Always uses the same GUID
                RoomName = "341",
                Capacity = 33,
                Availability = true,
                Equipment = toolsList
            };

            Console.WriteLine(room.ToJson());

            MeetingRooms.InsertOne(room);
        }


        public void SchemaCreator()
        {
            var requiredCollections = new List<string> { "MeetingRooms", "Reservations", "Reservers" };
            var existingCollections = _database.ListCollectionNames().ToList();

            Console.WriteLine($"Checking and creating schemas...");

            foreach (var collectionName in requiredCollections)
            {
                if (!existingCollections.Contains(collectionName)) // ✅ Create only if it doesn't exist
                {
                    Console.WriteLine($"Creating schema for {collectionName}...");

                    var validator = GetValidatorForCollection(collectionName); // ✅ Define schema for the collection

                    var options = new CreateCollectionOptions<BsonDocument>
                    {
                        Validator = new BsonDocumentFilterDefinition<BsonDocument>(validator),
                    };

                    _database.CreateCollection(collectionName, options);

                    Console.WriteLine($"Schema for {collectionName} created successfully.");
                }
                else
                {
                    Console.WriteLine($"Collection '{collectionName}' already exists. Skipping creation.");
                }
            }
        }

        private BsonDocument GetValidatorForCollection(string collectionName)
        {
            return collectionName switch
            {
                "MeetingRooms" => new BsonDocument
        {
            { "$jsonSchema", new BsonDocument
                {
                    { "bsonType", "object" },
                    { "required", new BsonArray { "Availability", "Capacity", "RoomName" } },
                    { "properties", new BsonDocument
                        {
                            { "Availability", new BsonDocument { { "bsonType", "bool" }, { "description", "'Availability' must be a Boolean and is required" } } },
                            { "Capacity", new BsonDocument { { "bsonType", "int" }, { "minimum", 0 }, { "description", "'Capacity' must be an Integer and is required" } } },
                            { "RoomName", new BsonDocument { { "bsonType", "string" }, { "maxLength", 50 }, { "description", "'RoomName' must be a String and is required" } } },
                            { "Equipment", new BsonDocument { { "bsonType", "array" }, { "description", "'Tools' must be an Array of Tool objects" } } }
                        }
                    }
                }
            }
        },

                "Reservations" => new BsonDocument
        {
            { "$jsonSchema", new BsonDocument
                {
                    { "bsonType", "object" },
                    { "required", new BsonArray { "RevNr", "RoomRevNr", "StartTime", "EndTime" } },
                    { "properties", new BsonDocument
                        {
                            { "RevNr", new BsonDocument { { "bsonType", "string" }, { "description", "'RevNr' must be a String and is required" } } },
                            { "RoomRevNr", new BsonDocument { { "bsonType", "string" }, { "description", "'RoomRevNr' must be a String and is required" } } },
                            { "StartTime", new BsonDocument { { "bsonType", "date" }, { "description", "'StartTime' must be a Date and is required" } } },
                            { "EndTime", new BsonDocument { { "bsonType", "date" }, { "description", "'EndTime' must be a Date and is required" } } },
                            { "Reason", new BsonDocument { { "bsonType", "string" }, { "maxLength", 100 }, { "description", "'Reason' must be a String" } } },
                        }
                    }
                }
            }
        },

                "Reservers" => new BsonDocument
        {
            { "$jsonSchema", new BsonDocument
                {
                    { "bsonType", "object" },
                    { "required", new BsonArray { "Name" } },
                    { "properties", new BsonDocument
                        {
                            { "Name", new BsonDocument { { "bsonType", "string" }, { "maxLength", 50 }, { "description", "'Name' must be a String and is required" } } }
                        }
                    }
                }
            }
        },

                _ => new BsonDocument() // ✅ Default empty schema
            };
        }

    }
}
