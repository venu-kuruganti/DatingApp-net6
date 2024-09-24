﻿using System;
using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
	public class ExceptionMiddleware
	{
		public RequestDelegate Next { get; set; }

		public ILogger<ExceptionMiddleware> Logger { get; set; }

		public IHostEnvironment Env { get; set; }

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
		{
			Next = next;
			Logger = logger;
			Env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await Next(context);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var response = Env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
					: new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

				var options = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};

				var json = JsonSerializer.Serialize(response, options);

				await context.Response.WriteAsync(json);
            }
		}
	}
}

