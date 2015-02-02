using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace ClubCloud.Afhangen.Behaviors
{
    [ContentProperty(Name = "Actions")]
    public abstract class TriggerBehavior<T> : Behavior<T> where T : DependencyObject
    {


        #region Actions Dependency Property

        /// <summary> 
        /// Actions collection 
        /// </summary> 
        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)base.GetValue(ActionsProperty);
                if (actions == null)
                {
                    actions = new ActionCollection();
                    base.SetValue(ActionsProperty, actions);
                }
                return actions;
            }
        }

        /// <summary> 
        /// Backing storage for Actions collection 
        /// </summary> 
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register("Actions",
                                        typeof(ActionCollection),
                                        typeof(TriggerBehavior<T>),
                                        new PropertyMetadata(null));

        #endregion Actions Dependency Property

        protected void Execute(object sender, object parameter)
        {
            Interaction.ExecuteActions(sender, this.Actions, parameter);
        }
    }
}
