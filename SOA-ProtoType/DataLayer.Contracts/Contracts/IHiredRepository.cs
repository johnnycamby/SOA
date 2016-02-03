using System.Collections.Generic;
using Business.Entities;
using Core.Contracts;
using DataLayer.Contracts.DTOs;

namespace DataLayer.Contracts.Contracts
{
    public interface IHiredRepository: IDataRepository<Hired>
    {
        IEnumerable<Hired> GetHireHistoryByDeveloper(int developerId);
        Hired GetCurrentHireByDeveloper(int developerId);
        IEnumerable<Hired> GetCurrentlyHiredDevelopers();
        IEnumerable<Hired> GetHireHistoryByAccount(int accountId);
        IEnumerable<ClientHireInfo> GetCurrentClientHireInfo();

    }
}