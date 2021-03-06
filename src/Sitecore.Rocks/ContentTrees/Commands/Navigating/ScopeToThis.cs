// � 2015-2016 Sitecore Corporation A/S. All rights reserved.

using System.Linq;
using Sitecore.Rocks.Commands;
using Sitecore.Rocks.Controls;
using Sitecore.Rocks.Data;
using Sitecore.Rocks.Extensibility;

namespace Sitecore.Rocks.ContentTrees.Commands.Navigating
{
    [Command, Feature(FeatureNames.Scopes)]
    public class ScopeToThis : CommandBase
    {
        public ScopeToThis()
        {
            Text = "Scope to This";
            Group = "Navigate";
            SortingValue = 5250;
        }

        public override bool CanExecute(object parameter)
        {
            var context = parameter as ContentTreeContext;
            if (context == null)
            {
                return false;
            }

            if (context.SelectedItems.Count() != 1)
            {
                return false;
            }

            var item = context.SelectedItems.First();
            if (!(item is IScopeable))
            {
                return false;
            }

            return item.Parent != null && !(item.Parent is MultiSelectTreeView);
        }

        public override void Execute(object parameter)
        {
            var context = parameter as ContentTreeContext;
            if (context == null)
            {
                return;
            }

            var item = context.SelectedItems.First() as IScopeable;
            if (item == null)
            {
                return;
            }

            var scopeItem = item.GetScopedTreeViewItem();

            context.ContentTree.PushScope(scopeItem);
        }
    }
}
