using AutoMapper;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using E_CommerceAPI.SERVICES.Repositories.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.UOW
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ECommerceDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public IAccountRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICartRepository Carts { get; private set; }
        public ICartItemsRepository CartItems { get; private set; }
        public IReviewRepository Reviews { get; private set; }
        public IWishlistRepository Wishlists { get; private set; }
        public IWishlistItemsRepository WishlistItems { get; private set; }
        public ISessionRepository Sessions { get; private set; }

        public UnitOfWork(ECommerceDbContext context, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _userManager=userManager;
            _mapper=mapper;
            _httpContextAccessor=httpContextAccessor;

            Customers = new AccountRepository(_context,_userManager,_configuration,_mapper, _httpContextAccessor);
            Carts = new CartRepository(_context,_mapper);
            CartItems = new CartItemsRepository(_context,_mapper);
            Categories= new CategoryRepository(_context,_mapper);
            Orders = new OrderRepository(_context,_mapper,_httpContextAccessor,_userManager);
            OrderItems= new OrderItemRepository(_context, _mapper, _httpContextAccessor, _userManager);
            Products = new ProductRepository(_context, _mapper);
            Reviews= new ReviewRepository(_context,_mapper);
            Wishlists=new WishlistRepository(_context,_mapper);
            WishlistItems=new WishlistItemsRepository(_context,_mapper);
            Sessions = new SessionRepository();

        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
