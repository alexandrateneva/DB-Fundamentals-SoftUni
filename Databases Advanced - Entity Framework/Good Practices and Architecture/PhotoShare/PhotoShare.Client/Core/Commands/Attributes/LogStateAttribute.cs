namespace PhotoShare.Client.Core.Commands.Attributes
{
    using System;
    using PhotoShare.Models;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LogStateAttribute : Attribute
    {
        private Log necessaryLogState;
        private Role role;

        public LogStateAttribute(Log necessaryLogState)
        {
            this.NecessaryLogState = necessaryLogState;
        }

        public Role Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        public Log NecessaryLogState
        {
            get { return this.necessaryLogState; }
            private set { this.necessaryLogState = value; }
        }

    }
}
