using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.PaymentDTO;
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
    public interface IPaymentRepository:IGenericRepository<Payment>
    {
        Task<ResponseDto> CreateCheckoutSession(PaymentDto dto, ApplicationUser user);

        public Task<ResponseDto> GetAllPayments();
        public Task<ResponseDto> GetPayment(int id);

        public Task<ResponseDto> DeletePayment(int id);

    }
}
