using InMemoryMongo2GoAndWireMockPoc.Models.ExternalResources.Response;
using InMemoryMongo2GoAndWireMockPoc.Models.Request;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryMongo2GoAndWireMockPoc.Domain.Entity
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string PostalCode { get; set; }
        [JsonProperty("Address")]
        public PostalCodeResponse Address { get; internal set; }

        public bool IsValidToCreate() => !GetErrosToCreate().Any();

        public IEnumerable<string> UserAlreadyCreatedMessage()
        {
            yield return "User already created.";
        }

        public IEnumerable<string> GetErrosToCreate()
        {
            if (string.IsNullOrEmpty(FirstName) || FirstName.All(char.IsWhiteSpace))
                yield return "First Name cannot be empty.";
            if (string.IsNullOrEmpty(LastName) || LastName.All(char.IsWhiteSpace))
                yield return "Last Name cannot be empty.";
            if (string.IsNullOrEmpty(Document) || Document.All(char.IsWhiteSpace))
                yield return "Document cannot be empty.";
        }

        public static User CreateNewUserFromModel(UserCreateModel userCreateModel) => new User()
        {
            Document = userCreateModel.Document,
            FirstName = userCreateModel.FirstName,
            LastName = userCreateModel.LastName,
            PostalCode = userCreateModel.PostalCode
        };
    }
}
