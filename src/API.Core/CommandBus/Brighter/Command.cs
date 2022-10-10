using System;
using System.Diagnostics;
using Paramore.Brighter;

namespace API.Core.CommandBus.Brighter
{
    public abstract class Command : ICommand
    {
        protected Command(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Span = new Activity(GetType().Name);
        }

        public Guid Id { get; set; }

        public Activity Span { get; set; }
    }
}
