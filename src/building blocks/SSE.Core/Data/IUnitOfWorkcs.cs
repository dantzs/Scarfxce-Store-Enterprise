using System;


namespace SSE.Core.Data
{
    public interface IUnitOfWorkc
    {
        Task<bool> Commit();
    }
}
