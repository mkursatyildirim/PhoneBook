using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Entities.Context;
using Report.API.Entities.Context;
using System;

namespace PhoneBook.Tests.Helpers
{
    public static class TestHelper
    {
        public static DbContextOptions<PhoneBookContext> GetPhoneBookContextForInMemoryDb()
        {
            return new DbContextOptionsBuilder<PhoneBookContext>()
                .UseInMemoryDatabase(databaseName: "PhoneBook" + DateTime.Now)
                .Options;
        }

        public static DbContextOptions<ReportContext> GetReportContextForInMemoryDb()
        {
            return new DbContextOptionsBuilder<ReportContext>()
                .UseInMemoryDatabase(databaseName: "Report" + DateTime.Now)
                .Options;
        }

        public static int? GetStatusCodeFromActionResult<T>(ActionResult<T> actionResult)
            => ((ObjectResult)actionResult.Result).StatusCode;
    }
}
