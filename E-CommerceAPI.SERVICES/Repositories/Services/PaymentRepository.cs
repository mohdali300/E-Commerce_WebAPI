using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.PaymentDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class PaymentRepository:GenericRepository<Payment> ,IPaymentRepository
    {
        private readonly IMapper _mapper;

        public PaymentRepository(ECommerceDbContext context, IOptions<StripeSettings> options, IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllPayments()
        {
            var pay = await _context.Payments.AsNoTracking().ToListAsync();
            if(pay!=null && pay.Count > 0)
            {
                var dto=_mapper.Map<List<PaymentDto>>(pay);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model= dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "User did not make any payments."
            };
        }

        public async Task<ResponseDto> GetPayment(int id)
        {
            var pay = await _context.Payments.FindAsync(id);
            if (pay != null)
            {
                var dto = _mapper.Map<PaymentDto>(pay);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = false,
                Message = "Payment not found."
            };
        }

        private async Task<Payment> CreatePayment(PaymentDto dto,ApplicationUser user)
        {
            var payment=_mapper.Map<Payment>(dto);
            payment.Customer = user;
            payment.CustomerId = user.Id;
            payment.Method = "card";

            await _context.Payments.AddAsync(payment);
            var entity=_context.Entry(payment);
            if (entity.State == EntityState.Added)
                return payment;
            return null;

        }

        public async Task<ResponseDto> CreateCheckoutSession(PaymentDto dto, ApplicationUser user)
        {
            var newPayment=CreatePayment(dto,user);
            if(newPayment == null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to create payment."
                };
            }

            if (!IsValidCurrency(newPayment.Result.Currency))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Currency is incorrect, enter invalid cuurency."
                };
            }

            var paymentDetails = new PaymentIntentCreateOptions()
            {
                Amount = (long)(newPayment.Result.Amount * 100),
                Currency = newPayment.Result.Currency,
                Description = newPayment.Result.Description,
                PaymentMethodTypes = new List<string> { newPayment.Result.Method },
                Customer=user.Id,
            };
            var returnedPayment = await new PaymentIntentService().CreateAsync(paymentDetails);

            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model= returnedPayment,
                Message = "Payment completed successfully."
            };
        }

        public async Task<ResponseDto> DeletePayment(int id)
        {
            var pay = await _context.Payments.FindAsync(id);
            if (pay != null)
            {
                var dto = _mapper.Map<PaymentDto>(pay);
                _context.Remove(pay);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }

            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = false,
                Message = "Payment not found."
            };
        }

        //---- Currency Validation --------

        private bool IsValidCurrency(string currency)
        {
            return SupportedCurrencies.Contains(currency.ToLower());
        }

        private readonly HashSet<string> SupportedCurrencies = new HashSet<string>
        {
            "usd", "aed", "afn", "all", "amd", "ang", "aoa", "ars", "aud", "awg", "azn", "bam",
            "bbd", "bdt", "bgn", "bhd", "bif", "bmd", "bnd", "bob", "brl", "bsd", "bwp", "byn",
            "bzd", "cad", "cdf", "chf", "clp", "cny", "cop", "crc", "cve", "czk", "djf", "dkk",
            "dop", "dzd", "egp", "etb", "eur", "fjd", "fkp", "gbp", "gel", "gip", "gmd", "gnf",
            "gtq", "gyd", "hkd", "hnl", "hrk", "htg", "huf", "idr", "ils", "inr", "isk", "jmd",
            "jod", "jpy", "kes", "kgs", "khr", "kmf", "krw", "kwd", "kyd", "kzt", "lak", "lbp",
            "lkr", "lrd", "lsl", "mad", "mdl", "mga", "mkd", "mmk", "mnt", "mop", "mur", "mvr",
            "mwk", "mxn", "myr", "mzn", "nad", "ngn", "nio", "nok", "npr", "nzd", "omr", "pab",
            "pen", "pgk", "php", "pkr", "pln", "pyg", "qar", "ron", "rsd", "rub", "rwf", "sar",
            "sbd", "scr", "sek", "sgd", "shp", "sle", "sos", "srd", "std", "szl", "thb", "tjs",
            "tnd", "top", "try", "ttd", "twd", "tzs", "uah", "ugx", "uyu", "uzs", "vnd", "vuv",
            "wst", "xaf", "xcd", "xof", "xpf", "yer", "zar", "zmw", "usdc", "btn", "ghs", "eek",
            "lvl", "svc", "vef", "ltl", "sll", "mro"
        };
    }
}
