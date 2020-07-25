using System;

namespace Api.Application.General
{
     public abstract class BaseCustomException : Exception
     {
          public string ErrorMessage { get; set; }
          public int StatusCode { get; set; }

          public BaseCustomException(string message, string error_message = "")
              : base(message)
          {
               ErrorMessage = error_message;

          }

          /// <summary>
          /// Erro utilizado para valores não encontrados no banco de dados para retornar uma mensagem tratada para a aplicação.
          /// </summary>
          public class CustomNotFoundException : BaseCustomException
          {
               public CustomNotFoundException(string message, string error_message = "")
                   : base(message, error_message)
               {
                    StatusCode = 404;
               }
          }

          /// <summary>
          /// Erro utilizado para argumentos inválidos passados para a API e retorna uma mensagem tratada para a aplicação.
          /// </summary>
          public class CustomBadRequestException : BaseCustomException
          {
               public CustomBadRequestException(string message, string error_message = "")
                   : base(message)
               {
                    StatusCode = 400;
               }
          }

          /// <summary>
          /// Erro utilizado para áreas do código que o usuário logado não tem acesso.
          /// </summary>
          public class CustomUnauthorizedException : BaseCustomException
          {
               public CustomUnauthorizedException(string message, string error_message = "")
                   : base(message)
               {
                    StatusCode = 401;
               }
          }
     }
}
