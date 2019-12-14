using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TestProject2.Constraints
{
    public class AccountConstraint : IHttpRouteConstraint
    {
        public static bool IsValidAccount(string sAccount)
        {
            return (!String.IsNullOrEmpty(sAccount) &&
                sAccount.StartsWith("1234") &&
                sAccount.Length > 5);
        }

        /// <summary>
        /// IHttpRouteConstraint.Match implementation to validate a parameter against
        /// the Enum members.  String comparison is NOT case-sensitive.
        /// </summary>
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                var stringVal = value as string;
                if (!String.IsNullOrEmpty(stringVal))
                {
                    if (IsValidAccount(stringVal))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}