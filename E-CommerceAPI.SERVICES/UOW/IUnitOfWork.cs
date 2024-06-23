﻿using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.UOW
{
    public interface IUnitOfWork:IDisposable
    {

        IAccountRepository Customers { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        ICartItemsRepository CartItems { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Wishlist> Wishlists { get; }
        IGenericRepository<WishlistItems> WishlistItems { get; }

        Task<int> Save();
    }
}
