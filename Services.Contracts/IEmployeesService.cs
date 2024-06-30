﻿using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDTO> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId,EmployeeParameters employeeParameters ,bool trackChanges);
        Task<EmployeeDTO> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        Task<EmployeeDTO> CreateEmployeeAsync(Guid companyId, EmployeeForCreationDTO employeeForCreation, bool trackChanges);
        Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id,
            EmployeeForUpdateDTO employeeForUpdate,
            bool compTrackChanges, bool empTrackChanges);
        Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid companyId, Guid id, bool compTrackChagnes, bool empTrackChanges);
        Task SaveChangeForPatchAsync(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity);
    }
}