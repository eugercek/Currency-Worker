namespace CurrencyWorker.Services.Contracts
{
    public interface IFetchData<T>
    {
        void DownloadData();
        T ReturnData();
    }
}