namespace Benaa.Core.Interfaces.IServices
{
    public interface IWalletService
    {
        //string IsPayed(decimal amount);

        Task<decimal> ChargeWallet(string ui, string code);

        Task<bool> Check(string u, decimal price);




    }
}