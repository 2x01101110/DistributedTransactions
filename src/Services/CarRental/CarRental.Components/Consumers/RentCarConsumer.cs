﻿using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Contracts.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Components.Consumers
{
    public class RentCarConsumer : IConsumer<IRentCar>
    {
        private readonly ILogger<RentCarConsumer> _logger;

        public RentCarConsumer(ILogger<RentCarConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IRentCar> context)
        {
            //throw new Exception("test");

            this._logger.LogInformation($"Processing {nameof(IRentCar)} event" +
                $"\r\nPayload: {JsonConvert.SerializeObject(context.Message)}");

            await context.RespondAsync<ICarRented>(new
            {
                context.Message.CarId,
                context.Message.RentFrom,
                context.Message.RentTo
            });
        }
    }
}