﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThothCore.Domain.Middleware
{
	public class JWTInHeaderMiddleware
	{
		private readonly RequestDelegate _next;

		public JWTInHeaderMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var name = "accessToken";
			var cookie = context.Request.Cookies[name];

			if (cookie != null)
				if (!context.Request.Headers.ContainsKey("Authorization"))
					context.Request.Headers.Append("Authorization", "Bearer " + cookie);

			await _next.Invoke(context);
		}
	}
}
