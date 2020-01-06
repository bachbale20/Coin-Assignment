using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment_Coin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Assignment_Coin.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }

        public class MyUserManager : UserManager<Account>
        {
            public MyUserManager(IUserStore<Account> store) : base(store)
            {
            }

            public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
            {
                var manager = new MyUserManager(new UserStore<Account>(new MyDbContext()));
                manager.UserValidator = new UserValidator<Account>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };

                // Configure user lockout defaults
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
                manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<Account>
                {
                    MessageFormat = "Your security code is {0}"
                });
                manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<Account>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });

                //manager.EmailService = new EmailService();
                //manager.SmsService = new SmsService();

                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<Account>(dataProtectionProvider.Create("ASP.NET Identity"));
                }

                return manager;
            }
        }
    }
}