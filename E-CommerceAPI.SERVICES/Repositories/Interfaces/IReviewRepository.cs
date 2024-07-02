using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IReviewRepository: IGenericRepository<Review>
    {
        public Task<ResponseDto> GetReview(int id);
        public Task<ResponseDto> GetAllCustomerReviews(ApplicationUser user);
        public Task<ResponseDto> GetAllProductReviews(int productId);
        public Task<ResponseDto> GetCustomerReviewOnProduct(ApplicationUser user, int prodId);

        public Task<ResponseDto> AddReview(Review review,ApplicationUser user);
        public Task<ResponseDto> UpdateReview(int id, Review review);
        public Task<ResponseDto> DeleteReview(int id);

    }
}
