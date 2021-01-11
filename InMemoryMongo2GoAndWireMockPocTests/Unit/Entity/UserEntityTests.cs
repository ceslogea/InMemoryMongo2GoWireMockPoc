using InMemoryMongo2GoAndWireMockPoc.Domain.Entity;
using InMemoryMongo2GoAndWireMockPoc.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Unit.Entity
{
    public class UserEntityTests
    {
        [Fact(DisplayName = "User sem firstName Quando verificado Deve retornar lista de erro")]
        public void User_without_firstName_When_checked_Should_return_error_list()
        {
            var user = User.CreateNewUserFromModel(new UserCreateModel() { Document = "32893956858", LastName = "LastName" });
            Assert.False(user.IsValidToCreate());
            Assert.NotEmpty(user.GetErrosToCreate());
        }

        [Fact(DisplayName = "User sem lastName Quando verificado Deve retornar lista de erro")]
        public void User_without_lastName_When_checked_Should_return_error_list()
        {
            var user = User.CreateNewUserFromModel(new UserCreateModel() { FirstName = "FirstName", Document = "32893956858" });
            Assert.False(user.IsValidToCreate());
            Assert.NotEmpty(user.GetErrosToCreate());
        }

        [Fact(DisplayName = "User sem document Quando verificado Deve retornar lista de erro")]
        public void User_without_document_When_checked_Should_return_error_list()
        {
            var user = User.CreateNewUserFromModel(new UserCreateModel() { FirstName = "FirstName", LastName = "LastName" });
            Assert.False(user.IsValidToCreate());
            Assert.NotEmpty(user.GetErrosToCreate());
        }

    }
}
