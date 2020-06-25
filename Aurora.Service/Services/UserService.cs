using System;
using System.Collections.Generic;
using Aurora.Domain.Entities;
using Aurora.Domain.Interfaces;
using Aurora.Service.Validators;
using FluentValidation;

namespace Aurora.Service.Services
{
    public class UserService : IServiceUser
    {
        private readonly IRepositoryUser _repositoryUser;

        public UserService(IRepositoryUser repositoryUser) =>
            _repositoryUser = repositoryUser;

        public IList<User> Browse() =>
            _repositoryUser.GetAll();

        public void Delete(int id) =>
            _repositoryUser.Remove(id);

        public User Insert(User obj)
        {
            Validate(obj, new UserValidator());
            _repositoryUser.Save(obj);
            return obj;
        }

        public User RecoverById(int id) =>
            _repositoryUser.GetById(id);

        public User Update(User obj)
        {
            Validate(obj, new UserValidator());
            _repositoryUser.Save(obj);
            return obj;
            throw new NotImplementedException();
        }

        private void Validate(User obj, UserValidator validator)
        {
            if (obj == null)
                throw new Exception("User not found!");

            validator.ValidateAndThrow(obj);
        }
    }
}
