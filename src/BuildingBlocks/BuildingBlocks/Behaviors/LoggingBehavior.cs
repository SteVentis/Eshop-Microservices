﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : 
	IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
										   where TResponse : notnull
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		logger.LogInformation("[START] Handle request={request} - Response={response} - RequestData={RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);

		var timer = new Stopwatch();
		timer.Start();

		var resposne = await next();

		timer.Stop();
		var timeTaken = timer.Elapsed;
		if(timeTaken.Seconds > 3)
		{
			logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds", typeof(TRequest), timeTaken.Seconds);
		}


		logger.LogInformation("[END] Handle request={request} - Response={response}", typeof(TRequest).Name, typeof(TResponse).Name);

		return resposne;
	}
}
