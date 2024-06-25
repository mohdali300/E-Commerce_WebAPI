using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class ReviewRepository:GenericRepository<Review>,IReviewRepository
    {
        private readonly IMapper _mapper;

        public ReviewRepository(ECommerceDbContext context, IMapper mapper):base(context)
        {
            _mapper= mapper;
        }

        public async Task<ResponseDto> GetReview(int id)
        {
            var review = await _context.Reviews.Where(r => r.Id == id)
                .Include(r => r.Product).FirstOrDefaultAsync();
            if(review != null)
            {
                var dto=_mapper.Map<ReviewDto>(review);
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
                Message="Review not found."
            };
        }

        public async Task<ResponseDto> GetAllCustomerReviews(ApplicationUser user)
        {
            var reviews = await _context.Reviews.Where(r=>r.CustomerId== user.Id)
                .Include(r=>r.Product)
                .ToListAsync();
            if (reviews != null && reviews.Count>0)
            {
                var dto = _mapper.Map<List<ReviewDto>>(reviews);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "There is no reviews by this user."
            };
        }

        public async Task<ResponseDto> GetAllProductReviews(int productId)
        {
            if (!_context.Products.Any(p => p.Id == productId))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "This product not found."
                };
            }
            var reviews = await _context.Reviews.Where(r => r.ProductId == productId)
                .Include(r => r.Product)
                .ToListAsync();
            if (reviews != null && reviews.Count > 0)
            {
                var dto = _mapper.Map<List<ReviewDto>>(reviews);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "There is no reviews for this product."
            };
        }

        public async Task<ResponseDto> GetCustomerReviewOnProduct(ApplicationUser user, int prodId)
        {
            if (!_context.Products.Any(p => p.Id == prodId))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "This product not found."
                };
            }
            var reviews = await _context.Reviews.Where(r => r.CustomerId == user.Id)
                .Include(r=>r.Product).Where(p=>p.ProductId == prodId)
                .ToListAsync();
            if (reviews != null && reviews.Count > 0)
            {
                var dto = _mapper.Map<List<ReviewDto>>(reviews);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "There is no reviews by this user for the product."
            };
        }

        public async Task<ResponseDto> AddReview(Review review, ApplicationUser user)
        {
            if (!_context.Products.Any(p => p.Id == review.ProductId))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Product not found."
                };
            }

            review.Customer = user;
            review.CustomerId=user.Id;
            review.Product = await _context.Products.FindAsync(review.ProductId);
            await _context.AddAsync(review);

            var entity=_context.Entry(review);
            if (entity.State == EntityState.Added)
            {
                var dto = _mapper.Map<ReviewDto>(review);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message="Failed to add review."
            };
        }

        public async Task<ResponseDto> UpdateReview(int id, Review review)
        {
            if (!_context.Reviews.Any(r=>r.Id==id))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Review not found."
                };
            }

            await _context.Reviews.Where(r => r.Id == id)
                .ExecuteUpdateAsync(s=>s.SetProperty(p=>p.Rate,review.Rate)
                .SetProperty(p => p.Comment, review.Comment)
                .SetProperty(p => p.Date, DateTime.UtcNow));

            var dto = _mapper.Map<ReviewDto>(review);
            dto.ProductName = await _context.Reviews.Where(r => r.Id == id)
                .Select(r => r.Product!.Name).FirstOrDefaultAsync();
            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model = dto
            };
        }

        public async Task<ResponseDto> DeleteReview(int id)
        {
            if (!_context.Reviews.Any(r => r.Id == id))
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Review not found."
                };
            }

            await _context.Reviews.Where(r => r.Id == id)
                .ExecuteDeleteAsync();
            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Message="Review deleted successfully."
            };
        }
    }
}
