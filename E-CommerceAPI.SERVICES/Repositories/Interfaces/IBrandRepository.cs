﻿using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IBrandRepository:IGenericRepository<Brand>
    {
        public Task<ResponseDto> GetAllBrands();
        public Task<ResponseDto> GetBrandById(int id);
        public Task<ResponseDto> GetBrandByName(string name);

        public Task<ResponseDto> AddBrand(BrandDto dto);
        public Task<ResponseDto> UpdateBrand(int id,BrandDto dto);
        public Task<ResponseDto> DeleteBrand(int id);
    }
}
