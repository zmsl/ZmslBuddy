using TreeSharp;

namespace ZmslBuddy.Profiles.Tags.Behavior
{
    public class ActionRunOnce : TreeSharp.Action
    {
        public ActionRunOnce(ActionDelegate actionDelegate)
            : base(actionDelegate)
        {
        }

        public ActionRunOnce(ActionSucceedDelegate actionSucceedDelegate)
            : base(actionSucceedDelegate)
        {
        }

        /// <summary>
        /// Run the object and change state to Complete
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override RunStatus Run(object context)
        {
            if (this.Complete)
            {
                return this.CompletionStatus;
            }

            this.Complete = true;

            return this.CompletionStatus = base.Run(context);
        }

        /// <summary>
        /// Gets whether the action is complete
        /// </summary>
        public bool Complete
        {
            get; 
            protected set;
        }

        /// <summary>
        /// Gets the run status returned by the only execution
        /// </summary>
        public RunStatus CompletionStatus
        {
            get;
            protected set; 
        }
    }
}
