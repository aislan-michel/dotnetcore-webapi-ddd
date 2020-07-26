using System;

namespace Api.Domain.Dtos.User
{
     public class UserDtoCreateResult
     {
          public long Id { get; set; }
          public string Name { get; set; }
          public string Email { get; set; }
          public DateTime CreateAt { get; set; }
     }
}
