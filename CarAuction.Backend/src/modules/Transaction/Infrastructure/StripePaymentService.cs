using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Configuration;

public class StripePaymentService : IPaymentService
{
    public StripePaymentService(IConfiguration configuration)
    {
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
    }

    public async Task<string> CreateCheckoutSessionAsync(Guid transactionId, decimal amount, string successUrl, string cancelUrl)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        UnitAmount = (long)(amount * 100), // Stripe expects the smallest currency unit (cents)
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Car Auction Winning Bid Payment"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            Metadata = new Dictionary<string, string>
            {
                { "transactionId", transactionId.ToString() }
            }
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return session.Url;
    }
}