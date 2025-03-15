namespace DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService
{
    using MediatR;
    using System;

    namespace DentalClinicManagement.CustomerServiceFeatures.DeleteCustomerService
    {
        public class DeleteCustomerServiceCommand : IRequest<bool>
        {
            public Guid Id { get; }

            public DeleteCustomerServiceCommand(Guid id)
            {
                Id = id;
            }
        }
    }

}
