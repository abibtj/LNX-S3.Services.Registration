﻿using System;
using S3.Common.Messages;
using Newtonsoft.Json;

namespace S3.Services.Registration.Utility
{
    public class PersonDeletedEvent : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public PersonDeletedEvent(Guid id) => Id = id;
    }
}
