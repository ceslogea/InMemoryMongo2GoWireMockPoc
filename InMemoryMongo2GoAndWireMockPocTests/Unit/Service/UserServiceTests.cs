using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Unit.Service
{
    public class UserServiceTests
    {
        public UserServiceTests()
        {

        }

        [Fact(DisplayName = "Deve inserir usuário Por dados de usuário válidos sem código postal Deve retornar usuário")]
        public void Should_insert_user_By_valid_user_data_without_posta_code_Must_retun_user()
        {

        }

        [Fact(DisplayName = "Deve inserir usuário Por dados de usuário válidos com código postal Deve retornar usuário")]
        public void Should_insert_user_By_valid_user_data_with_posta_code_Must_retun_user()
        {

        }

        [Fact(DisplayName = "Não deve inserir o usuário Documento de usuário ja cadastrado Deve retornar mensagem de erro")]
        public void Should_not_insert_user_User_document_already_registered_Must_return_error_message()
        {

        }

        [Fact(DisplayName = "Não deve inserir o usuário Erro no serviço do usuário Deve retornar mensagem de exceção")]
        public void Should_not_insert_user_Error_in_user_service_Must_return_exception_message()
        {

        }

    }
}
