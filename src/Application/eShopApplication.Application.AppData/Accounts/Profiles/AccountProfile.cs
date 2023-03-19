using System;
using AutoMapper;
using eShopApplication.Contracts.Accounts;


namespace eShopApplication.Application.AppData.Account.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<eShopApplication.Domain.Account, ReadAccountDto>();
            CreateMap<CreateAccountDto, eShopApplication.Domain.Account>();
        }
    }
}
