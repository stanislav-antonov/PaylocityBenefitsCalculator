using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaycheckIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForPaycheck_ShouldReturnCorrectPaycheck()
    {
        var response = await HttpClient.GetAsync("/api/v1/paycheck/1");
        var employee = new GetPaycheckDto
        {
            EmployeeId = 1,
            PayPeriods = 26,
            EmployeeGrossPay = 2900.81m,
            EmployeeNetPay = 2439.27m,
            EmployeeGrossSalary = 75420.99m,
            EmployeeNetSalary = 63420.99m
        };
        
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
}



