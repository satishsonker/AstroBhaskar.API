using AstroBhaskar.API.Config;
using AstroBhaskar.API.Constants;
using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Exceptions;
using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using AstroBhaskar.API.Services;
using AstroBhaskar.API.Services.Interfaces;
using AutoFixture;
using AutoMapper;
using Bjk.ModelLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AstroBhaskar.API.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ICommonRepository> _commonRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly UserServices _userServices;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _fixture = new Fixture();
            _commonRepositoryMock = new Mock<ICommonRepository>();
            _mapper = new MapperConfiguration(c => c.AddProfile<MappingProfile>()).CreateMapper();
            _userServices = new UserServices(_userRepositoryMock.Object, _commonRepositoryMock.Object, _mapper);
        }

        [Fact(DisplayName = "User Service - Add_Success - Should be able to add user.")]
        public async void Add_Success()
        {
            //arrange
            UserRequest userRequest = _fixture.Build<UserRequest>().Create();
            _commonRepositoryMock.Setup(x => x.IsUserExist(It.IsAny<string>())).ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.Add(It.IsAny<AstroUser>())).ReturnsAsync(1);

            //act
            var result = await _userServices.Add(userRequest);

            //assert
            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "User Service - Add_UserAlreadyExist - Should throw the business exception if user already exist")]
        public async void Add_UserAlreadyExist()
        {
            //arrange
            UserRequest userRequest = _fixture.Build<UserRequest>().Create();
            _commonRepositoryMock.Setup(x => x.IsUserExist(It.IsAny<string>())).ReturnsAsync(true);

            //act
            BusinessRuleViolationException businessRuleViolationException = await Assert.ThrowsAsync<BusinessRuleViolationException>(
                async () => await _userServices.Add(userRequest));

            //assert
            Assert.NotNull(businessRuleViolationException);
            Assert.Equal(StaticValues.UserAlreadyExist, businessRuleViolationException.ErrorResponseType);
            Assert.Equal(StaticValues.UserAlreadyExistMessage, businessRuleViolationException.Message);
        }

        [Fact(DisplayName = "User Service - Add_ThrowExceptionIfUserNull - Should throw the business exception if user id null")]
        public async void Add_ThrowExceptionIfUserNull()
        {
            //arrange
            UserRequest userRequest = It.IsAny<UserRequest>();

            //act
            BusinessRuleViolationException businessRuleViolationException = await Assert.ThrowsAsync<BusinessRuleViolationException>(
                async ()=>await _userServices.Add(userRequest));

            //assert
            Assert.NotNull(businessRuleViolationException);
            Assert.Equal(StaticValues.UserRequired, businessRuleViolationException.ErrorResponseType);
            Assert.Equal(StaticValues.UserRequiredMessage, businessRuleViolationException.Message);
        }

        [Fact(DisplayName = "User Service - Delete_Success - Should be able to delete user")]
        public async void Delete_Success()
        {
            //arrange
            int userId = 1;
            _commonRepositoryMock.Setup(x => x.IsUserExist(userId)).ReturnsAsync(true);
            _userRepositoryMock.Setup(x => x.Delete(userId)).ReturnsAsync(1);

            //act
            var result = await _userServices.Delete(userId);

            //assert
            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "User Service - Delete_UserNotExist - Should throw the business exception if user not exist")]
        public async void Delete_UserNotExist()
        {
            //arrange
            int userId = 1;
            _commonRepositoryMock.Setup(x => x.IsUserExist(userId)).ReturnsAsync(false);

            //act
            BusinessRuleViolationException businessRuleViolationException = await Assert.ThrowsAsync<BusinessRuleViolationException>(
                async () => await _userServices.Delete(userId));

            //assert
            Assert.NotNull(businessRuleViolationException);
            Assert.Equal(StaticValues.UserNotFound, businessRuleViolationException.ErrorResponseType);
            Assert.Equal(StaticValues.UserNotFoundMessage, businessRuleViolationException.Message);
        }
    }
}
