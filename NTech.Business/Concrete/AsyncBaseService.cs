﻿using AutoMapper;
using Core.DataAccess;
using Core.Dto;
using Core.Entity;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;

namespace NTech.Business.Concrete
{
    public class AsyncBaseService<TEntity, TWriteDto, TReadDto> : IAsyncBaseService<TEntity, TWriteDto, TReadDto>
        where TEntity : class, IEntity, new()
        where TWriteDto : class, IWriteDto, new()
        where TReadDto : class, IReadDto, new()
    {

        protected readonly IAsyncRepository<TEntity> Repository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;
        public AsyncBaseService(IAsyncRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public async Task<IResult> AddAsync(TWriteDto dto)
        {
            TEntity addedEntity = Mapper.Map<TEntity>(dto);
            await Repository.AddAsync(addedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult() :
                new ErrorResult();
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            TEntity deletedEntity = await Repository.GetAsync(x => x.Id == id);
            if (deletedEntity == null)
                return new ErrorResult();

            await Repository.DeleteAsync(deletedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult() :
                new ErrorResult();
        }

        public async Task<IDataResult<TReadDto>> GetByIdAsync(int id)
        {
            TEntity entity = await Repository.GetAsync(x => x.Id == id);
            if (entity == null)
                return new ErrorDataResult<TReadDto>();

            TReadDto returnEntity = Mapper.Map<TReadDto>(entity);
            return new SuccessDataResult<TReadDto>(returnEntity);
        }

        public async Task<IDataResult<List<TReadDto>>> GetListAsync()
        {
            List<TEntity> entities = await Repository.GetAll().ToListAsync();
            List<TReadDto> returnEntities = Mapper.Map<List<TReadDto>>(entities);

            return new SuccessDataResult<List<TReadDto>>(returnEntities);
        }

        public async Task<IResult> UpdateAsync(int id, TWriteDto dto)
        {
            TEntity updatedEntity = await Repository.GetAsync(x => x.Id == id);
            if (updatedEntity == null)
                return new ErrorDataResult<TWriteDto>();

            Mapper.Map(dto, updatedEntity);
            await Repository.UpdateAsync(updatedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult() :
                new ErrorResult();
        }
    }
}