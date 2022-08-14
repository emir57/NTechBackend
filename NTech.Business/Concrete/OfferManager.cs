﻿using AutoMapper;
using Core.DataAccess;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using NTech.Business.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Concrete
{
    public class OfferManager : AsyncBaseService<Offer, OfferWriteDto, OfferReadDto>, IOfferService
    {
        public OfferManager(IAsyncRepository<Offer> repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage) : base(repository, mapper, unitOfWork, languageMessage)
        {
        }

        public override Task<IResult> AddAsync(OfferWriteDto dto)
        {
            return base.AddAsync(dto);
        }

        public override Task<IResult> UpdateAsync(int id, OfferWriteDto dto)
        {
            return base.UpdateAsync(id, dto);
        }
    }
}