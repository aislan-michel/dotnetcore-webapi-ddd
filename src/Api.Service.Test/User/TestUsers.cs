using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.User
{
     public class TestUsers
     {
          public static long FakerId { get; set; }
          public static string FakerName { get; set; }
          public static string FakerEmail { get; set; }

          public static string FakerNameUpdated { get; set; }
          public static string FakerEmailUpdated { get; set; }


          public List<UserDto> FakerUserDtoList = new List<UserDto>();
          public UserDto FakerUserDto;

          public UserDtoCreate FakerUserDtoCreate;
          public UserDtoCreateResult FakerUserDtoCreateResult;

          public UserDtoUpdate FakerUserDtoUpdate;
          public UserDtoUpdateResult FakerUserDtoUpdateResult;

          public TestUsers()
          {
               FakerId = Faker.RandomNumber.Next();
               FakerName = Faker.Name.FullName();
               FakerEmail = Faker.Internet.Email();

               FakerNameUpdated = Faker.Name.FullName();
               FakerEmailUpdated = Faker.Internet.Email();

               for(int index = 0; index < 10; index++)
               {
                    var dto = new UserDto()
                    {
                         Id = Faker.RandomNumber.Next(),
                         Name = Faker.Name.FullName(),
                         Email = Faker.Internet.Email()
                    };

                    FakerUserDtoList.Add(dto);
               }

               FakerUserDto = new UserDto()
               {
                    Id = FakerId,
                    Name = FakerName,
                    Email = FakerEmail
               };

               FakerUserDtoCreate = new UserDtoCreate()
               {
                    Name = FakerName,
                    Email = FakerEmail
               };

               FakerUserDtoCreateResult = new UserDtoCreateResult()
               {
                    Id = FakerId,
                    Name = FakerName,
                    Email = FakerEmail,
                    CreateAt = DateTime.UtcNow
               };

               FakerUserDtoUpdate = new UserDtoUpdate()
               {
                    Id = FakerId,
                    Name = FakerName,
                    Email = FakerEmail
               };

               FakerUserDtoUpdateResult = new UserDtoUpdateResult()
               {
                    Id = FakerId,
                    Name = FakerName,
                    Email = FakerEmail,
                    UpdateAt = DateTime.UtcNow
               };
          }
     }
}
