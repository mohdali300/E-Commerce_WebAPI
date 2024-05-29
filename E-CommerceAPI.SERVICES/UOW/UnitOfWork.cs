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
        public IGenericRepository<Cart> Carts { get; private set; }
        public IGenericRepository<CartItems> CartItems { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public IGenericRepository<OrderItems> OrderItems { get; private set; }
        public IGenericRepository<Review> Reviews { get; private set; }
        public IGenericRepository<Wishlist> Wishlists { get; private set; }
        public IGenericRepository<WishlistItems> WishlistItems { get; private set; }

        public UnitOfWork(ECommerceDbContext context, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _userManager=userManager;
            _mapper=mapper;
            _httpContextAccessor=httpContextAccessor;

            Customers = new AccountRepository(_context,_userManager,_configuration,_mapper, _httpContextAccessor);
            Carts = new GenericRepository<Cart>(_context);
            CartItems = new GenericRepository<CartItems>(_context);
            Categories= new GenericRepository<Category>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderItems= new GenericRepository<OrderItems>(_context);
            Products = new ProductRepository(_context, _mapper);
            Reviews= new GenericRepository<Review>(_context);
            Wishlists=new GenericRepository<Wishlist>(_context);
            WishlistItems=new GenericRepository<WishlistItems>(_context);

        }

        public async Task<int> Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
