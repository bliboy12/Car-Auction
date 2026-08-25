public interface IPaymentService
{
    Task<string> CreateCheckoutSessionAsync(Guid transactionId, decimal amount, string successUrl, string cancelUrl);
}