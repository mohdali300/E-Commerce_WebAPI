using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class SessionRepository:ISessionRepository
    {
        public SessionRepository()
        {
            
        }

        public Task<Session> CreateCheckoutSession()
        {
            throw new NotImplementedException();
        }
    }
}
