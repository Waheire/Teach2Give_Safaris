﻿using Coupon_Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace Coupon_Service.Extensions
{
    public static class AddMigration
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope()) 
            {
                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                {
                    _db.Database.Migrate();
                }
            }
            return app;
        }
    }
}
