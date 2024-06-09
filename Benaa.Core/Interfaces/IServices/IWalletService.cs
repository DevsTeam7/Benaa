using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IWalletService
    {
        Task<ErrorOr<decimal>> ChargeWallet(string userId, string code);
		Task<bool> Check(string u, decimal price);
        Task<object> SetPayment(Guid itemID, string type, decimal price, string ui);
        Task<Guid> CraeteWallet();
        Task RefundUser(decimal amount, string studentId);
        Task<ErrorOr<decimal>> GetUserAmount(string userId);
	}
}