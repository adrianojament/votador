﻿using api.domain.Dtos.Usuario;
using api.domain.Dtos.Usuario.Standard;
using api.domain.Dtos.Validation;
using api.domain.Entities;
using api.domain.Interfaces.Repositories;
using api.domain.Interfaces.Services;
using api.shared.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.services.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuariosRepository _repository;
        private readonly IMapper _mapper;

        public UsuariosServices(IUsuariosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioDto>> Get()
        {
            return _mapper.Map<IEnumerable<UsuarioDto>>(await _repository.SelectAsync());
        }

        public async Task<UsuarioDto> Get(Guid id)
        {
            return _mapper.Map<UsuarioDto>(await _repository.SelectAsync(id));
        }

        public async Task<UsuarioDto> GetUserEmail(string email)
        {
            var entityUser = await _repository.getEmail(email);
            if (entityUser == null)
                return null;
            return _mapper.Map<UsuarioDto>(entityUser);
        }

        public async Task<UsuarioDtoCreateResult> Post(UsuarioDtoCreate usuario)
        {
            var entity = _mapper.Map<UsuarioEntity>(usuario);
            return _mapper.Map<UsuarioDtoCreateResult>(await _repository.InsertAsync(entity));
        }

        public async Task<UsuarioDtoUpdateResult> Put(UsuarioDtoUpdate usuario)
        {
            var entity = _mapper.Map<UsuarioEntity>(usuario);
            return _mapper.Map<UsuarioDtoUpdateResult>(await _repository.UpdateAsync(entity));
        }

        public async Task<DtoValidacao> ValidateEmailPassword(string email, string password)
        {
            var dtoValidacao = new DtoValidacao();
            dtoValidacao.Sucesso = true;
            var entityUsers = await _repository.getEmail(email);
            if (entityUsers == null)
            {
                dtoValidacao.Sucesso = false;
                dtoValidacao.Mensagem = "e-Mail não encontrado";
                return dtoValidacao;
            }

            if (!entityUsers.Senha.Equals(password))
            {
                dtoValidacao.Sucesso = false;
                dtoValidacao.Mensagem = "Senha Inválida";
                return dtoValidacao;
            }

            return dtoValidacao;
        }

        public async Task<DtoValidacao> ValidationEmailsAlreadyExist(string email, Guid id)
        {

            var result = await _repository.SelectAsync(u => u.eMail.Equals(email));
            var entity = result.FirstOrDefault();
            var dtoValidacao = new DtoValidacao();
            dtoValidacao.Sucesso = true;

            if (entity != null && !entity.Id.Equals(id))
            {
                dtoValidacao.Sucesso = false;
                dtoValidacao.Mensagem = "e-Mail já cadastrado";
            }

            return dtoValidacao;
        }       
    }
}
