using System.Collections.Generic;
using Business.Entities;
using Core.Contracts;
using DataLayer.Contracts.DTOs;

namespace DataLayer.Contracts.Contracts
{
    public interface IHiringRepository: IDataRepository<Hired>
    {
        IEnumerable<Hired> GetHiringHistoryByDeveloper(int developerId);
        Hired GetCurrentHiringByDeveloper(int developerId);
        IEnumerable<Hired> GetCurrentlyHiredDevelopers();
        IEnumerable<Hired> GetHiringHistoryByAccount(int accountId);
        IEnumerable<ClientHireInfo> GetCurrentClientHiringInfo();

    }
}